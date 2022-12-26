using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
  public Transform container;
  public Transform shopItemTemplate;
  public GameObject moneyDisplay;

  private int money = 4;
  private int shop_items = 3;
  private AbilityMeta[] abilities;

  // Start is called before the first frame update
  void Start() {
    abilities = AbilityMeta.getAll();
    roll();
  }

  // Update is called once per frame
  void Update() {
    // update TMP text
    moneyDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Money: " + money.ToString();
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

      shopItem.Find("icon").GetComponent<Image>().sprite = ability.icon;
    }
  }
}
