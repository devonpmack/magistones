using System;
using System.Collections.Generic;
using UnityEngine;

public class PersistencyManager : MonoBehaviour {
  [Serializable]
  public struct Data {
    [Serializable]
    public struct OwnedAbility {
      public string abilityName;
      public int level;
    }

    public int lives;
    public int wins;
    public int money;

    [SerializeField]
    public List<OwnedAbility> ownedAbilities;

    public Data(int money) {
      this.money = money;
      lives = 3;
      wins = 0;
      ownedAbilities = new List<OwnedAbility>();
    }
  }

  public Data data;

  public static Data? load() {
    if (!PlayerPrefs.HasKey("data")) {
      return null;
    }

    return JsonUtility.FromJson<Data>(PlayerPrefs.GetString("data"));
  }

  public static void save(Data data) {
    PlayerPrefs.SetString("data", JsonUtility.ToJson(data));
  }
}
