using UnityEngine;

public class AbilityDisplay : Fusion.NetworkBehaviour {
  public Ability ability;
  public GameObject cooldown;

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

    // round to 1 decimal place, if 0 then show ''
    float? remaining_cooldown = ability.cooldown_remaining.RemainingTime(Runner);
    cooldown.GetComponent<TMPro.TextMeshProUGUI>().text = remaining_cooldown.HasValue && remaining_cooldown.Value >= 0.001 ? remaining_cooldown.Value.ToString("0.0") : "";

  }

}
