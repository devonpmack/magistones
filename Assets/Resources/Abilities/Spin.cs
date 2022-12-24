using UnityEngine;

public class Spin : Ability {
  // public GameObject spinPrefab;

  public override float cooldown {
    get {
      return 0.5f;
    }
  }

  protected override void onCast(NetworkInputPrototype input) {
    transform.LookAt(new Vector3(input.mouse_x, transform.position.y, input.mouse_z));

    // Runner.Spawn(rockPrefab,
    //   transform.position, transform.rotation,
    //   GetComponent<NetworkObject>().InputAuthority, (runner, o) => {
    //     // Initialize the Ball before synchronizing it
    //     o.GetComponent<Laser>().Init(GetComponent<Wizard>().Id);
    //   });
  }
}
