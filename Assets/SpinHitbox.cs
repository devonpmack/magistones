using Fusion;

public class SpinHitbox : NetworkBehaviour {
  public float lifeTime;

  [Networked] private NetworkBehaviourId source { get; set; }

  [Networked] private TickTimer life { get; set; }

  public void Init(NetworkBehaviourId sourceId) {
    source = sourceId;
    life = TickTimer.CreateFromSeconds(Runner, lifeTime);
    gameObject.SetActive(true);
  }

  public override void FixedUpdateNetwork() {
    if (life.ExpiredOrNotRunning(Runner))
      gameObject.SetActive(false);
  }

  // when it collides with a player push themm
  // [System.Obsolete]
  // private void OnTriggerEnter(Collider other) {
  //   if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Wizard>().Id != source) {
  //     Wizard wiz = other.gameObject.GetComponent<Wizard>();

  //     other.gameObject.GetComponent<NetworkCharacterControllerPrototype>().Velocity = (transform.forward * 8 * wiz.damageMultiplier());
  //     wiz.Damage += 10;
  //     wiz.stun_remaining = TickTimer.CreateFromSeconds(Runner, 0.4f);

  //     Runner.Despawn(Object);
  //   }
  // }
}
