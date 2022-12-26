using UnityEngine;

[CreateAssetMenuAttribute]
public class AbilityMeta : ScriptableObject {
  public Sprite icon;

  public static AbilityMeta[] getAll() {
    return Resources.LoadAll<AbilityMeta>("Abilities");
  }
}
