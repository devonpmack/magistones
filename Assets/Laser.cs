using Fusion;

public class Laser : NetworkBehaviour
{
  public float velocity;
  public float lifeTime;
  [Networked] private TickTimer life { get; set; }

  public void Init()
  {
    life = TickTimer.CreateFromSeconds(Runner, lifeTime);
  }

  public override void FixedUpdateNetwork()
  {
    if (life.Expired(Runner))
      Runner.Despawn(Object);
    else
      transform.position += velocity * transform.forward * Runner.DeltaTime;
  }

}
