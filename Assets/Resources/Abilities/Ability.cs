using UnityEngine;

public abstract class Ability : MonoBehaviour {
  abstract public float cooldown { get; }
  abstract protected void onCast(Transform player);

  public float cooldown_remaining = 0.0f;

  public void cast() {
    if (cooldown_remaining <= 0.01f) {
      onCast(transform);

      cooldown_remaining = cooldown;
    }
  }

  void Update() {
    if (cooldown_remaining > 0.0f) {

      cooldown_remaining -= Time.deltaTime;
    }
  }
}
