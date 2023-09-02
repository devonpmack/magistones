using Fusion;
using UnityEngine;

public class FieryFist : Ability
{
  public PushHitbox punchHitbox;

  protected override void onCast(NetworkInputPrototype input)
  {
    transform.LookAt(new Vector3(input.mouse_x, transform.position.y, input.mouse_z));

    Runner.Spawn(punchHitbox,
      transform.position, transform.rotation,
      GetComponent<NetworkObject>().StateAuthority, (runner, o) =>
      {
        o.GetComponent<PushHitbox>().Init(Object.InputAuthority);
        o.transform.SetParent(transform);
      });
  }
}
