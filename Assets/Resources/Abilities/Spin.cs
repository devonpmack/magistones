using Fusion;

public class Spin : Ability {
  public SpinHitbox spinHitbox;

  protected override void onCast(NetworkInputPrototype input) {
    Runner.Spawn(spinHitbox,
      transform.position, transform.rotation,
      GetComponent<NetworkObject>().InputAuthority, (runner, o) => {
        o.GetComponent<SpinHitbox>().Init(Object.InputAuthority);
        o.transform.SetParent(transform);
      });
  }
}
