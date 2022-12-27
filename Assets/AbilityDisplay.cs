using UnityEngine;

public class AbilityDisplay : MonoBehaviour {
  public GameObject cooldown;

  // Update is called once per frame
  public void UpdateCooldown(float? remainingTime) {
    if (remainingTime <= 0 || !remainingTime.HasValue) {
      GetComponent<CanvasRenderer>().SetAlpha(1.0f);
    } else {
      GetComponent<CanvasRenderer>().SetAlpha(0.2f);
    }

    // round to 1 decimal place, if 0 then show ''
    cooldown.GetComponent<TMPro.TextMeshProUGUI>().text = remainingTime.HasValue && remainingTime.Value >= 0.001 ? remainingTime.Value.ToString("0.0") : "";
  }

}
