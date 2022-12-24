using UnityEngine;

public class Blink : Ability {
  public override float cooldown {
    get {
      return 3f;
    }
  }

  public float blinkDistance = 3f;

  protected override void onCast(NetworkInputPrototype input) {
    transform.LookAt(new Vector3(input.mouse_x, transform.position.y, input.mouse_z));
    transform.position += transform.forward * blinkDistance;
  }
}
