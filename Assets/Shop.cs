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
    this.ownedAbilities = new List<OwnedAbility>();
    roll(true);

    load();
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

        // 0 = "" 1 = "*" 2 = "**"
        ownedAbilities[i].transform.Find("stars").GetComponent<TMPro.TextMeshProUGUI>().text = new string('*', ++ownedAbilities[i].level);

        money--;
        Destroy(shopItem.gameObject);
        return;
      }
    }

    ownedAbilities.Add(new OwnedAbility {
      abilityName = abilityName,
      level = 0,
      transform = shopItem
    });
    money--;
    shopItem.SetParent(ownedContainer);
    shopItem.GetComponent<Button>().onClick.RemoveAllListeners();
    shopItem.GetComponent<Button>().onClick.AddListener(() => {
      sell(shopItem.transform, abilityName);
      save();
    });
  }

  public void sell(Transform shopItem, string abilityName) {
    for (int i = 0; i < ownedAbilities.Count; i++) {
      if (ownedAbilities[i] != null && ownedAbilities[i].abilityName == abilityName) {
        money += ownedAbilities[i].level + 1;
        Destroy(shopItem.gameObject);
        ownedAbilities.RemoveAt(i);
        return;
      }
    }
  }

  public void roll(bool free = false) {
    if (money <= 0)
      return;

    if (!free)
      money -= 1;

    // remove all shop items
    foreach (Transform child in container) {
      GameObject.Destroy(child.gameObject);
    }

    for (int i = 0; i < shop_items; i++) {
      var shopItem = Instantiate(shopItemTemplate, container);
      var ability = abilities[Random.Range(0, abilities.Length)];

      Button buttonCtrl = shopItem.GetComponent<Button>();
      buttonCtrl.onClick.AddListener(() => {
        purchase(shopItem.transform, ability.name);
        save();
      });

      shopItem.Find("icon").GetComponent<Image>().sprite = ability.icon;
    }
  }

  public void save() {
    PersistencyManager.Data data = new PersistencyManager.Data(money);

    foreach (OwnedAbility ownedAbility in ownedAbilities) {
      data.ownedAbilities.Add(new PersistencyManager.Data.OwnedAbility {
        abilityName = ownedAbility.abilityName,
        level = ownedAbility.level
      });

      data.money = money;
    }

    PersistencyManager.save(data);
  }

  public void load() {
    var loaded = PersistencyManager.load();
    if (loaded == null)
      return;

    var data = (PersistencyManager.Data)loaded;
    money = data.money;

    foreach (PersistencyManager.Data.OwnedAbility ownedAbility in data.ownedAbilities) {
      var shopItem = Instantiate(shopItemTemplate, ownedContainer);
      var ability = AbilityMeta.get(ownedAbility.abilityName);
      this.ownedAbilities.Add(new OwnedAbility {
        abilityName = ability.name,
        level = ownedAbility.level,
        transform = shopItem
      });

      Button buttonCtrl = shopItem.GetComponent<Button>();
      buttonCtrl.onClick.AddListener(() => { sell(shopItem.transform, ability.name); save(); });

      shopItem.Find("icon").GetComponent<Image>().sprite = ability.icon;
      shopItem.Find("stars").GetComponent<TMPro.TextMeshProUGUI>().text = new string('*', ownedAbility.level);
    }
  }
}
