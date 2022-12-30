using Fusion;
using UnityEngine;

public class SpinHitbox : NetworkBehaviour {
  public float lifeTime;

  [Networked] private PlayerRef source { get; set; }

  [Networked] private TickTimer life { get; set; }

  public void Init(PlayerRef sourceId) {
    source = sourceId;
    life = TickTimer.CreateFromSeconds(Runner, lifeTime);
  }

  public override void Spawned() {
    transform.SetParent(Runner.GetPlayerObject(source).transform);
    transform.localPosition = Vector3.zero;
  }

  public override void FixedUpdateNetwork() {
    // right before timer expires push back nearby players
    if (Object.HasStateAuthority && life.Expired(Runner) && gameObject.activeSelf) {
      foreach (var player in GameObject.FindGameObjectsWithTag("Player")) {
        // todo check networked position against collider
        if (!player.GetComponent<Collider>().bounds.Intersects(GetComponent<Collider>().bounds))
          continue;

        if (player.GetComponent<NetworkObject>().InputAuthority != source) {
          Wizard wiz = player.GetComponent<Wizard>();

          player.GetComponent<NetworkCharacterControllerPrototype>().Velocity = (player.transform.position - transform.position).normalized * 10 * wiz.damageMultiplier();
          wiz.Damage += 10;
          wiz.stun_remaining = TickTimer.CreateFromSeconds(Runner, 0.4f);
        }
      }
    }

    if (life.ExpiredOrNotRunning(Runner))
      Runner.Despawn(Object);
  }
}
