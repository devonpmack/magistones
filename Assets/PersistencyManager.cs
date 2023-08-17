using System;
using System.Collections.Generic;
using UnityEngine;

public class PersistencyManager : MonoBehaviour
{

  public const int STARTING_MONEY = 5;
  public const int INCOME = 3;

  public const int MAX_LIVES = 3;
  public const int MAX_WINS = 6;

  [Serializable]
  public struct Data
  {
    [Serializable]
    public struct OwnedAbility
    {
      public string abilityName;
      public int level;
    }

    public bool rollOnLoad;
    public List<string> shop;
    public int lives;
    public int wins;
    public int money;

    [SerializeField]
    public List<OwnedAbility> ownedAbilities;

    public Data(int money)
    {
      this.money = money;
      shop = null;
      lives = MAX_LIVES;
      wins = 0;
      ownedAbilities = new List<OwnedAbility>();
      rollOnLoad = false;
    }
  }

  public Data data;

  public static bool IsClone()
  {
    string[] s = Application.dataPath.Split('/');
    string projectName = s[s.Length - 2];

    return projectName == "magistones_clone_0";
  }

  public static Data load()
  {
    Debug.Log(PlayerPrefs.GetString("data"));
    if (IsClone())
    {
      return JsonUtility.FromJson<Data>("{\"shop\":[],\"lives\":3,\"wins\":0,\"money\":3,\"ownedAbilities\":[{\"abilityName\":\"Rock Throw\",\"level\":0},{\"abilityName\":\"Spin\",\"level\":1},{\"abilityName\":\"Final Chapter\",\"level\":1},{\"abilityName\":\"Blink\",\"level\":0}]}");
    }

    if (!PlayerPrefs.HasKey("data"))
    {
      var data = new Data(STARTING_MONEY);
      data.ownedAbilities.Add(new Data.OwnedAbility() { abilityName = "None", level = 0 });
      data.ownedAbilities.Add(new Data.OwnedAbility() { abilityName = "None", level = 0 });
      data.ownedAbilities.Add(new Data.OwnedAbility() { abilityName = "None", level = 0 });
      data.ownedAbilities.Add(new Data.OwnedAbility() { abilityName = "None", level = 0 });

      return data;
    }

    return JsonUtility.FromJson<Data>(PlayerPrefs.GetString("data"));
  }

  public static void LoseLifeAndSave()
  {
    var data = load();
    data.lives--;
    data.money += INCOME;
    data.rollOnLoad = true;
    Debug.Log(JsonUtility.ToJson(data));

    if (data.lives <= 0)
    {
      Shop.Reset();
    }
    else
    {
      save(data);
    }
  }

  public static void WinAndSave()
  {
    var data = load();
    data.wins++;
    save(data);
  }


  public static void save(Data data)
  {
    if (IsClone()) return;

    PlayerPrefs.SetString("data", JsonUtility.ToJson(data));
  }

  public static void Clear()
  {
    PlayerPrefs.DeleteKey("data");
  }
}
