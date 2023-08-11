using Fusion;
using UnityEngine;

public class WizardHealthManager : NetworkBehaviour
{

  private GameObject safeZone;

  public bool IsOnLava()
  {
    return Vector3.Distance(transform.position, safeZone.transform.position) > 12;
  }


  // Start is called before the first frame update
  public override void Spawned()
  {
    safeZone = GameObject.FindGameObjectWithTag("SafeFloor");
  }

  // Update is called once per frame
  public override void FixedUpdateNetwork()
  {

    if (!HasStateAuthority)
    {
      return;
    }

    Wizard wiz = GetComponent<Wizard>();

    // if 10 units away from safezone
    if (IsOnLava())
    {
      if (wiz.Damage >= 150)
      {
        transform.position = new Vector3(5, 5, 14);
        wiz.Damage = 0;
      }
      else
      {
        wiz.Damage += 0.2f;
      }
    }
  }
}
