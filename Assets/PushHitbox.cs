using Fusion;
using UnityEngine;

public class PushHitbox : NetworkBehaviour
{
  public float lifeTime;
  public float Damage;

  [Networked] private PlayerRef source { get; set; }

  [Networked] private TickTimer life { get; set; }

  public void Init(PlayerRef sourceId)
  {
    source = sourceId;
    life = TickTimer.CreateFromSeconds(Runner, lifeTime);
  }

  public override void Spawned()
  {
    Debug.Log(source);
    Debug.Log(Runner.GetPlayerObject(source));
    Debug.Log(transform);
    transform.SetParent(Runner.GetPlayerObject(source).transform);
    transform.localPosition = Vector3.zero;
  }

  public override void FixedUpdateNetwork()
  {
    // right before timer expires push back nearby players
    if (Object.HasStateAuthority && life.Expired(Runner) && gameObject.activeSelf)
    {
      foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
      {
        // todo check networked position against collider
        if (!player.GetComponent<Collider>().bounds.Intersects(GetComponent<Collider>().bounds))
          continue;

        if (player.GetComponent<NetworkObject>().InputAuthority != source)
        {
          player.GetComponent<Wizard>().RPC_DoDamage(Damage, transform.forward);
        }
      }
    }

    if (life.ExpiredOrNotRunning(Runner))
      Runner.Despawn(Object);
  }
}
