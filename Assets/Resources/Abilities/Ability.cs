using Fusion;
using UnityEngine;

public abstract class Ability : Fusion.NetworkBehaviour {
  abstract public float cooldown { get; }

  abstract protected void onCast(NetworkInputPrototype input);

  [Networked] public TickTimer cooldown_remaining { get; set; }

  [HideInInspector]
  public AbilityDisplay display;

  public void cast(NetworkInputPrototype input) {
    if (cooldown_remaining.ExpiredOrNotRunning(Runner)) {
      onCast(input);

      cooldown_remaining = TickTimer.CreateFromSeconds(Runner, cooldown); ;
    }
  }

  public override void FixedUpdateNetwork() {
    if (display == null) return;

    display.UpdateCooldown(cooldown_remaining.RemainingTime(Runner));
  }
}
