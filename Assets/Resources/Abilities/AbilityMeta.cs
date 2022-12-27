using System.Linq;
using UnityEngine;

[CreateAssetMenuAttribute]
public class AbilityMeta : ScriptableObject {
  public Sprite icon;
  public new string name;

  public static AbilityMeta[] getAll() {
    return Resources.LoadAll<AbilityMeta>("Abilities");
  }

  public static AbilityMeta get(string name) {
    return getAll().First(ability => ability.name == name);
  }
}
