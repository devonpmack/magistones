using UnityEngine;

public class AbilityDisplay : Fusion.NetworkBehaviour {
  public Ability ability;

  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    if (!ability) return;

    if (ability.cooldown_remaining.ExpiredOrNotRunning(Runner)) {
      GetComponent<CanvasRenderer>().SetAlpha(1.0f);
    } else {
      GetComponent<CanvasRenderer>().SetAlpha(0.2f);
    }
  }
}
