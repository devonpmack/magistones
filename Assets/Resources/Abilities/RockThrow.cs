using UnityEngine;

public class RockThrow : Ability {
  public override float cooldown {
    get {
      return .5f;
    }
  }

  protected override void onCast(Transform player) {

  }
}
