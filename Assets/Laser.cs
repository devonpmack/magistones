using Fusion;
using UnityEngine;

public class Laser : NetworkBehaviour {
  public float velocity;
  public float lifeTime;
  [Networked] private TickTimer life { get; set; }

  public void Init() {
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

    Debug.Log("Collided with: " + other.gameObject.name);

    if (other.gameObject.tag == "Player" && !other.gameObject.GetComponent<NetworkObject>().HasInputAuthority) {
      other.gameObject.GetComponent<NetworkCharacterControllerPrototype>().Velocity = (transform.forward * 10);

      Runner.Despawn(Object);
    }
  }
}
