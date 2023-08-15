using System;
using System.Collections.Generic;
using UnityEngine;

public class PersistencyManager : MonoBehaviour
{

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
    }
  }

  public Data data;

  public static Data load()
  {
    string[] s = Application.dataPath.Split('/');
    string projectName = s[s.Length - 2];


    if (projectName == "magistones_clone_0")
    {
      return JsonUtility.FromJson<Data>("{\"shop\":[],\"lives\":3,\"wins\":0,\"money\":3,\"ownedAbilities\":[{\"abilityName\":\"Rock Throw\",\"level\":0},{\"abilityName\":\"Spin\",\"level\":1},{\"abilityName\":\"Final Chapter\",\"level\":1},{\"abilityName\":\"Blink\",\"level\":0}]}");
    }

    if (!PlayerPrefs.HasKey("data"))
    {
      var data = new Data(10);
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
    save(data);
  }

  public static void WinAndSave()
  {
    var data = load();
    data.wins++;
    save(data);
  }

  public static void RestartGame()
  {
    clear();
    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
  }



  public static void save(Data data)
  {
    PlayerPrefs.SetString("data", JsonUtility.ToJson(data));
  }

  public static void clear()
  {
    PlayerPrefs.DeleteKey("data");
  }
}
