using System.Collections;
using Fusion;
using UnityEngine;

public class FinalChapter : Ability {
  public GameObject hitbox;
  public float spawnDelay;

  public override float cooldown {
    get {
      return 1f;
    }
  }

  protected override void onCast(NetworkInputPrototype input) {
    transform.LookAt(new Vector3(input.mouse_x, transform.position.y, input.mouse_z));

    StartCoroutine(spawnProjectiles(transform.rotation));
  }

  private IEnumerator spawnProjectiles(Quaternion rotation) {
    spawnProjectile(rotation);
    yield return new WaitForSeconds(spawnDelay);
    spawnProjectile(rotation);
    yield return new WaitForSeconds(spawnDelay);
    spawnProjectile(rotation);
  }

  private void spawnProjectile(Quaternion rotation) {
    Runner.Spawn(hitbox,
      transform.position, rotation,
      GetComponent<NetworkObject>().InputAuthority, (runner, o) => {
        // Initialize the Ball before synchronizing it
        o.GetComponent<Projectile>().Init(GetComponent<Wizard>().Id);
      });
  }

}
