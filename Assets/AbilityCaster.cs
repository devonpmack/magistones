using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityCaster : MonoBehaviour
{
  public Ability ability;
  // Start is called before the first frame update
  void Start()
  {
    // view = GetComponent<PhotonView>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Mouse.current.leftButton.wasPressedThisFrame)
    {
      ability.onCast(transform);
    }
  }
}
