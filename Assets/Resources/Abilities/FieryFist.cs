using System.Numerics;
using Fusion;
using UnityEngine;

public class FieryFist : Ability
{
  public PushHitbox punchHitbox;

  protected override void onCast(NetworkInputPrototype input)
  {
    Runner.Spawn(punchHitbox,
      transform.position, transform.rotation,
      GetComponent<NetworkObject>().StateAuthority, (runner, o) =>
      {
        o.GetComponent<PushHitbox>().Init(Object.InputAuthority);
        o.transform.SetParent(transform);
      });

    GetComponent<Wizard>().stun_remaining = TickTimer.CreateFromSeconds(Runner, 0.8f);
    GetComponent<NetworkCharacterControllerPrototype>().Velocity = UnityEngine.Vector3.zero;
  }
}
