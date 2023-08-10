using Fusion;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
  public float velocity;
  public float lifeTime;
  public float damage;

  [Networked] private PlayerRef source { get; set; }

  [Networked] private TickTimer life { get; set; }

  public void Init(PlayerRef sourceId)
  {
    source = sourceId;
    life = TickTimer.CreateFromSeconds(Runner, lifeTime);
  }
  public override void FixedUpdateNetwork()
  {

    if (!HasStateAuthority) return;

    if (life.Expired(Runner))
    {
      Runner.Despawn(Object);
      return;
    }
    else
    {
      transform.position += velocity * transform.forward * Runner.DeltaTime;
    }

    // if colliding with another player, do 10 damage
    foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
    {
      if (!player.GetComponent<Collider>().bounds.Intersects(GetComponent<Collider>().bounds))
        continue;

      if (player.GetComponent<NetworkObject>().InputAuthority != source)
      {
        Wizard wiz = player.GetComponent<Wizard>();
        wiz.RPC_DoDamage(damage, transform.forward);
        Runner.Despawn(Object);
      }
    }
  }
}
