using Fusion;
using UnityEngine;

public abstract class Ability : Fusion.NetworkBehaviour
{
  public AbilityMeta meta;
  public int level = 0;

  abstract protected void onCast(NetworkInputPrototype input);

  [Networked] public TickTimer cooldown_remaining { get; set; }

  [HideInInspector]
  public AbilityDisplay display;

  public void cast(NetworkInputPrototype input)
  {
    if (meta.name == "None") return;

    if (cooldown_remaining.ExpiredOrNotRunning(Runner))
    {
      onCast(input);

      cooldown_remaining = TickTimer.CreateFromSeconds(Runner, meta.cooldown); ;
    }
  }

  public override void FixedUpdateNetwork()
  {
    if (display == null) return;

    display.UpdateCooldown(cooldown_remaining.RemainingTime(Runner));
  }
}
