using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
  private class OwnedAbility {
    public string abilityName;
    public int level;
    public Transform transform;
  }

  public GameObject sellIndicator;
  public List<string> inShop;

  public Transform container;
  public Transform ownedContainer;
  public Transform shopItemTemplate;
  public GameObject moneyDisplay;

  private int money = 10;
  private int shop_items = 3;
  private AbilityMeta[] abilities;
  private List<OwnedAbility> ownedAbilities;
  public GameObject noAbilityPrefab;

  // Start is called before the first frame update
  void Start() {
    abilities = AbilityMeta.getAll();
    this.ownedAbilities = new List<OwnedAbility>();

    load();
  }

  // Update is called once per frame
  void Update() {
    // update TMP text
    moneyDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Money: " + money.ToString();
  }

  public void reorderAbility(string abilityName, int index) {
    var toMove = ownedAbilities.Find(a => a.abilityName == abilityName);

    // remove toMove from list
    ownedAbilities.Remove(toMove);

    // add toMove to new list at index
    ownedAbilities.Insert(index, toMove);



    save();
  }


  public void purchase(Transform shopItem, string abilityName) {
    if (money <= 0)
      return;

    // if already in the array, just increment level
    for (int i = 0; i < ownedAbilities.Count; i++) {
      if (ownedAbilities[i] != null && ownedAbilities[i].abilityName == abilityName) {
        if (ownedAbilities[i].level >= 2)
          return;

        // 0 = "" 1 = "*" 2 = "**"
        ownedAbilities[i].transform.Find("stars").GetComponent<TMPro.TextMeshProUGUI>().text = new string('*', ++ownedAbilities[i].level);

        money--;
        Destroy(shopItem.gameObject);
        inShop.Remove(abilityName);
        save();
        return;
      }
    }

    // if there are already 4 abilities, check if any are None and delete one
    if (ownedAbilities.Count >= 4) {
      for (int i = 0; i < ownedAbilities.Count; i++) {
        if (ownedAbilities[i] != null && ownedAbilities[i].abilityName == "None") {
          Destroy(ownedAbilities[i].transform.gameObject);
          ownedAbilities.RemoveAt(i);
          break;
        }
      }
    }

    // if there are still 4 abilities, don't add another
    if (ownedAbilities.Count >= 4)
      return;

    ownedAbilities.Add(new OwnedAbility {
      abilityName = abilityName,
      level = 0,
      transform = shopItem
    });

    money--;
    shopItem.SetParent(ownedContainer);
    shopItem.GetComponent<Button>().onClick.RemoveAllListeners();
    shopItem.GetComponent<Draggable>().shop = this;
    shopItem.GetComponent<Draggable>().isDraggable = true;
    inShop.Remove(abilityName);
    save();
  }

  public void sell(Transform shopItem, string abilityName) {
    for (int i = 0; i < ownedAbilities.Count; i++) {
      if (ownedAbilities[i] != null && ownedAbilities[i].abilityName == abilityName) {
        money += ownedAbilities[i].level + 1;
        Destroy(shopItem.gameObject);
        ownedAbilities[i].abilityName = "None";
        ownedAbilities[i].level = 0;
        ownedAbilities[i].transform = Instantiate(noAbilityPrefab, ownedContainer).transform;

        save();
        return;
      }
    }


    save();
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

    inShop = new List<string>();

    for (int i = 0; i < shop_items; i++) {
      var shopItem = Instantiate(shopItemTemplate, container);
      var ability = abilities[Random.Range(0, abilities.Length)];

      Button buttonCtrl = shopItem.GetComponent<Button>();
      buttonCtrl.onClick.AddListener(() => {
        purchase(shopItem.transform, ability.name);
      });
      inShop.Add(ability.name);

      OwnedAbilitySetup(shopItem, ability.icon, 0, ability.name);
      shopItem.GetComponent<Draggable>().isDraggable = false;
    }

    save();
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

    data.shop = inShop;

    PersistencyManager.save(data);
  }

  public void load() {
    var data = PersistencyManager.load();
    money = data.money;

    if (data.shop == null) {
      roll(true);
    } else {
      inShop = data.shop;

      foreach (string abilityName in inShop) {
        var shopItem = Instantiate(shopItemTemplate, container);
        var ability = AbilityMeta.get(abilityName);

        Button buttonCtrl = shopItem.GetComponent<Button>();
        buttonCtrl.onClick.AddListener(() => {
          purchase(shopItem.transform, ability.name);
        });

        OwnedAbilitySetup(shopItem, ability.icon, 0, ability.name);
        shopItem.GetComponent<Draggable>().isDraggable = false;
      }
    }

    foreach (PersistencyManager.Data.OwnedAbility ownedAbility in data.ownedAbilities) {
      if (ownedAbility.abilityName == "None") {
        var placeholder = Instantiate(noAbilityPrefab, ownedContainer);
        this.ownedAbilities.Add(new OwnedAbility {
          abilityName = "None",
          level = 0,
          transform = placeholder.transform
        });
        continue;
      }

      var shopItem = Instantiate(shopItemTemplate, ownedContainer);
      var ability = AbilityMeta.get(ownedAbility.abilityName);
      this.ownedAbilities.Add(new OwnedAbility {
        abilityName = ability.name,
        level = ownedAbility.level,
        transform = shopItem
      });

      OwnedAbilitySetup(shopItem, ability.icon, ownedAbility.level, ability.name);
    }
  }

  private void OwnedAbilitySetup(Transform shopItem, Sprite icon, int level, string name) {
    shopItem.GetComponent<Draggable>().shop = this;
    shopItem.GetComponent<Draggable>().abilityName = name;
    shopItem.Find("icon").GetComponent<Image>().sprite = icon;
    shopItem.Find("stars").GetComponent<TMPro.TextMeshProUGUI>().text = new string('*', level);
  }
}
