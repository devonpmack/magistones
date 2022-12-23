using Fusion;
using UnityEngine;

public class Laser : NetworkBehaviour {
  public float velocity;
  public float lifeTime;

  private int source;

  [Networked] private TickTimer life { get; set; }

  public void Init(int sourceId) {
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
    Debug.Log("Collided with: " + other.gameObject.name);

    if (other.gameObject.tag == "Player" && other.gameObject.GetInstanceID() != source) {
      other.gameObject.GetComponent<NetworkCharacterControllerPrototype>().Velocity = (transform.forward * 10);

      Runner.Despawn(Object);
    }
  }
}
