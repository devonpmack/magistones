using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
  private class OwnedAbility {
    public string abilityName;
    public int level;
    public Transform transform;
  }


  public Transform container;
  public Transform ownedContainer;
  public Transform shopItemTemplate;
  public GameObject moneyDisplay;

  private int money = 10;
  private int shop_items = 3;
  private AbilityMeta[] abilities;
  private List<OwnedAbility> ownedAbilities;

  // Start is called before the first frame update
  void Start() {
    abilities = AbilityMeta.getAll();
    ownedAbilities = new List<OwnedAbility>();
    roll();
  }

  // Update is called once per frame
  void Update() {
    // update TMP text
    moneyDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Money: " + money.ToString();
  }

  public void purchase(Transform shopItem, string abilityName) {
    if (money <= 0)
      return;

    // if already in the array, just increment level
    for (int i = 0; i < ownedAbilities.Count; i++) {
      if (ownedAbilities[i] != null && ownedAbilities[i].abilityName == abilityName) {
        if (ownedAbilities[i].level >= 3)
          return;

        ownedAbilities[i].transform.Find("stars").GetComponent<TMPro.TextMeshProUGUI>().text = (++ownedAbilities[i].level).ToString();
        money--;
        Destroy(shopItem.gameObject);
        return;
      }
    }

    ownedAbilities.Add(new OwnedAbility {
      abilityName = abilityName,
      level = 1,
      transform = shopItem
    });
    money--;
    shopItem.SetParent(ownedContainer);
  }

  public void roll() {
    if (money <= 0)
      return;

    money -= 1;

    // remove all shop items
    foreach (Transform child in container) {
      GameObject.Destroy(child.gameObject);
    }

    for (int i = 0; i < shop_items; i++) {
      var shopItem = Instantiate(shopItemTemplate, container);
      var ability = abilities[Random.Range(0, abilities.Length)];

      Button buttonCtrl = shopItem.GetComponent<Button>();
      buttonCtrl.onClick.AddListener(() => purchase(shopItem.transform, ability.name));

      shopItem.Find("icon").GetComponent<Image>().sprite = ability.icon;
    }
  }
}
