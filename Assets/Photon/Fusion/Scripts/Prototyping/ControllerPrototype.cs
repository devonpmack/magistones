
using Fusion;
using UnityEngine;

[ScriptHelp(BackColor = EditorHeaderBackColor.Steel)]
public class ControllerPrototype : Fusion.NetworkBehaviour {
  protected NetworkCharacterControllerPrototype _ncc;
  protected NetworkRigidbody _nrb;
  protected NetworkRigidbody2D _nrb2d;
  protected NetworkTransform _nt;
  protected Wizard _wz;
  public bool bot = false;

  [SerializeField] private Laser _prefabBall;

  [Networked]
  public Vector3 MovementDirection { get; set; }

  public bool TransformLocal = false;

  [DrawIf(nameof(ShowSpeed), Hide = true)]
  public float Speed = 6f;

  bool ShowSpeed => this && !TryGetComponent<NetworkCharacterControllerPrototype>(out _);

  public void Awake() {
    CacheComponents();
  }

  public override void Spawned() {
    CacheComponents();
  }

  private void CacheComponents() {
    if (!_ncc) _ncc = GetComponent<NetworkCharacterControllerPrototype>();
    if (!_nrb) _nrb = GetComponent<NetworkRigidbody>();
    if (!_nrb2d) _nrb2d = GetComponent<NetworkRigidbody2D>();
    if (!_nt) _nt = GetComponent<NetworkTransform>();
    if (!_wz) _wz = GetComponent<Wizard>();
  }

  public override void FixedUpdateNetwork() {
    if (Runner.Config.PhysicsEngine == NetworkProjectConfig.PhysicsEngines.None) {
      return;
    }

    if (bot) {
      transform.LookAt(new Vector3(4.5f, transform.position.y, 13));
      _ncc.Move(transform.forward);

      // if there are more than 2  gameobject tagged player, destroy myself
      if (GameObject.FindGameObjectsWithTag("Player").Length > 2) {
        NetworkObject.Destroy(gameObject);
      }
    }

    Vector3 direction;
    if (GetInput(out NetworkInputPrototype input)) {
      direction = default;

      if (input.IsDown(NetworkInputPrototype.BUTTON_FORWARD)) {
        direction += TransformLocal ? transform.forward : Vector3.forward;
      }

      if (input.IsDown(NetworkInputPrototype.BUTTON_BACKWARD)) {
        direction -= TransformLocal ? transform.forward : Vector3.forward;
      }

      if (input.IsDown(NetworkInputPrototype.BUTTON_LEFT)) {
        direction -= TransformLocal ? transform.right : Vector3.right;
      }

      if (input.IsDown(NetworkInputPrototype.BUTTON_RIGHT)) {
        direction += TransformLocal ? transform.right : Vector3.right;
      }

      direction = direction.normalized;

      MovementDirection = direction;
    } else {
      direction = MovementDirection;
    }

    if (_ncc) {
      _ncc.Move(direction);
    } else if (_nrb && !_nrb.Rigidbody.isKinematic) {
      _nrb.Rigidbody.AddForce(direction * Speed);
    } else if (_nrb2d && !_nrb2d.Rigidbody.isKinematic) {
      Vector2 direction2d = new Vector2(direction.x, direction.y + direction.z);
      _nrb2d.Rigidbody.AddForce(direction2d * Speed);
    } else {
      transform.position += (direction * Speed * Runner.DeltaTime);
    }

    if (input.IsDown(NetworkInputPrototype.BUTTON_PRIMARY)) {
      _wz.abilities[0].cast(input);
    }

    if (input.IsDown(NetworkInputPrototype.BUTTON_SECONDARY)) {
      _wz.abilities[1].cast(input);
    }

    if (input.IsDown(NetworkInputPrototype.BUTTON_TERTIARY)) {
      _wz.abilities[2].cast(input);
    }

    if (input.IsDown(NetworkInputPrototype.BUTTON_QUATERNARY)) {
      _wz.abilities[3].cast(input);
    }



    if (transform.position.y < -10) {
      transform.position = new Vector3(5, 5, 14);
      _wz.Damage = 0;
    }

  }
}
