public class Spin : Ability {
  public SpinHitbox spinHitbox;

  public override float cooldown {
    get {
      return 4f;
    }
  }

  protected override void onCast(NetworkInputPrototype input) {
    spinHitbox.gameObject.GetComponent<SpinHitbox>().Init(GetComponent<Wizard>().Id);
  }
}
