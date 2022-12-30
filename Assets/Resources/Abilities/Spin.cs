using Fusion;

public class Spin : Ability {
  public SpinHitbox spinHitbox;

  public override float cooldown {
    get {
      return 4f;
    }
  }

  protected override void onCast(NetworkInputPrototype input) {
    Runner.Spawn(spinHitbox,
      transform.position, transform.rotation,
      GetComponent<NetworkObject>().InputAuthority, (runner, o) => {
        o.GetComponent<SpinHitbox>().Init(GetComponent<Wizard>().Id);
        o.transform.SetParent(transform);
      });
  }
}
