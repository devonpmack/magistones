using Fusion;
using UnityEngine;

public class Haste : Ability
{
  public float haste = 1.5f;
  public float length = 4f;

  protected override void onCast(NetworkInputPrototype input)
  {
    GetComponent<Wizard>().haste_remaining = TickTimer.CreateFromSeconds(Runner, length);
  }
}
