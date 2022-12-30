using System.Linq;
using UnityEngine;

[CreateAssetMenuAttribute]
public class AbilityMeta : ScriptableObject {
  public Sprite icon;
  public new string name;
  public string description;
  public int cooldown;

  public static AbilityMeta[] getAll() {
    return Resources.LoadAll<AbilityMeta>("Abilities");
  }

  public static AbilityMeta get(string name) {
    return getAll().First(ability => ability.name == name);
  }

  public void setTooltip(SimpleTooltip tooltip, int level = 0) {
    tooltip.infoLeft = name + " " + new string('*', level) + "\n\n" + description;
    tooltip.infoRight = "Cooldown: " + cooldown + "s";
  }
}
