using UnityEngine;

public class AbilityDisplay : MonoBehaviour {
  public Ability ability;

  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    if (!ability) return;

    if (ability.cooldown_remaining > 0) {
      GetComponent<CanvasRenderer>().SetAlpha(0.5f);
    } else {
      GetComponent<CanvasRenderer>().SetAlpha(1.0f);
    }
  }
}
