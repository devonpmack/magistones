using UnityEngine;
using UnityEngine.UI;

public class AbilityDisplayContainer : MonoBehaviour {
  public GameObject abilityDisplayTemplate;
  public GameObject noAbilityPrefab;

  private string[] hotkeys = { "LMB", "RMB", "LSHIFT", "SPACE" };

  void Start() {
    var data = PersistencyManager.load();

    var i = 0;
    foreach (var ownedAbility in data.ownedAbilities) {

      if (ownedAbility.abilityName == "None") {
        Instantiate(noAbilityPrefab, transform);
        continue;
      }

      var ability = AbilityMeta.get(ownedAbility.abilityName);
      var abilityDisplay = Instantiate(abilityDisplayTemplate, transform);

      abilityDisplay.GetComponent<Image>().sprite = ability.icon;
      abilityDisplay.transform.Find("hotkey").GetComponent<TMPro.TextMeshProUGUI>().text = hotkeys[i++];
      ability.setTooltip(abilityDisplay.GetComponent<SimpleTooltip>(), ownedAbility.level);
    }
  }

}
