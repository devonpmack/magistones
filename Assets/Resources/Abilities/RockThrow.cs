using Fusion;
using UnityEngine;

public class RockThrow : Ability {
  protected override void onCast(NetworkInputPrototype input) {
    transform.LookAt(new Vector3(input.mouse_x, transform.position.y, input.mouse_z));

    Runner.Spawn(Resources.Load<GameObject>("Abilities/RockHitbox"),
      transform.position, transform.rotation,
      GetComponent<NetworkObject>().InputAuthority, (runner, o) => {
        // Initialize the Ball before synchronizing it
        o.GetComponent<Projectile>().Init(Object.InputAuthority);
      });
  }
}
