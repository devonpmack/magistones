using Fusion;
using UnityEngine;

public class Laser : NetworkBehaviour {
  public float velocity;
  public float lifeTime;

  [Networked] private NetworkBehaviourId source { get; set; }

  [Networked] private TickTimer life { get; set; }

  public void Init(NetworkBehaviourId sourceId) {
    source = sourceId;
    life = TickTimer.CreateFromSeconds(Runner, lifeTime);
  }

  public override void FixedUpdateNetwork() {
    if (life.Expired(Runner))
      Runner.Despawn(Object);
    else
      transform.position += velocity * transform.forward * Runner.DeltaTime;
  }

  // when it collides with a player push themm
  [System.Obsolete]
  private void OnTriggerEnter(Collider other) {
    // todo check networked position against collider
    if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Wizard>().Id != source) {
      Wizard wiz = other.gameObject.GetComponent<Wizard>();

      other.gameObject.GetComponent<NetworkCharacterControllerPrototype>().Velocity = (transform.forward * 8 * wiz.damageMultiplier());
      wiz.Damage += 10;
      wiz.stun_remaining = TickTimer.CreateFromSeconds(Runner, 0.4f);

      Runner.Despawn(Object);
    }
  }
}
