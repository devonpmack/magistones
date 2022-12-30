using Fusion;
using UnityEngine;

public class Projectile : NetworkBehaviour {
  public float velocity;
  public float lifeTime;
  public float damage;

  [Networked] private PlayerRef source { get; set; }

  [Networked] private TickTimer life { get; set; }

  public void Init(PlayerRef sourceId) {
    source = sourceId;
    life = TickTimer.CreateFromSeconds(Runner, lifeTime);
  }
  public override void FixedUpdateNetwork() {
    if (Object.HasStateAuthority) {
      if (life.Expired(Runner)) {
        Runner.Despawn(Object);
        return;
      } else {
        transform.position += velocity * transform.forward * Runner.DeltaTime;
      }
    }

    // if colliding with another player, do 10 damage
    foreach (var player in GameObject.FindGameObjectsWithTag("Player")) {
      if (!player.GetComponent<Collider>().bounds.Intersects(GetComponent<Collider>().bounds))
        continue;

      if (player.GetComponent<NetworkObject>().InputAuthority != source) {
        Wizard wiz = player.GetComponent<Wizard>();

        if (player.GetComponent<NetworkObject>().HasStateAuthority) {
          player.GetComponent<NetworkCharacterControllerPrototype>().Velocity = transform.forward * damage * wiz.damageMultiplier();
          wiz.stun_remaining = TickTimer.CreateFromSeconds(Runner, 0.4f);
          wiz.Damage += Mathf.Abs((int)damage);

          Runner.Despawn(Object);
        }
      }
    }
  }
}
