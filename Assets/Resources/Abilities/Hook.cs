using Fusion;
using UnityEngine;

public class Hook : Ability {
  public Projectile projectile;

  protected override void onCast(NetworkInputPrototype input) {
    transform.LookAt(new Vector3(input.mouse_x, transform.position.y, input.mouse_z));

    Runner.Spawn(projectile,
      transform.position, transform.rotation,
      GetComponent<NetworkObject>().InputAuthority, (runner, o) => {
        o.GetComponent<Projectile>().Init(Object.InputAuthority);
      });
  }
}
