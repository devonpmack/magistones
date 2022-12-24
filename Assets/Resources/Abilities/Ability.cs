using Fusion;

public abstract class Ability : Fusion.NetworkBehaviour {
  abstract public float cooldown { get; }
  abstract protected void onCast(NetworkInputPrototype input);

  [Networked] public TickTimer cooldown_remaining { get; set; }

  public void cast(NetworkInputPrototype input) {
    if (cooldown_remaining.ExpiredOrNotRunning(Runner)) {
      onCast(input);

      cooldown_remaining = TickTimer.CreateFromSeconds(Runner, cooldown); ;
    }
  }
}
