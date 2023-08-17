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

  [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
  public void RPC_GiveWin()
  {
    Debug.Log("Received!");
    PersistencyManager.WinAndSave();
  }

  // Update is called once per frame
  public override void FixedUpdateNetwork()
  {

    if (!HasStateAuthority || GetComponent<ControllerPrototype>().bot)
    {
      return;
    }

    Wizard wiz = GetComponent<Wizard>();

    // if 10 units away from safezone
    if (IsOnLava())
    {
      if (wiz.Damage >= 150)
      {
        if (GetComponent<ControllerPrototype>().bot)
        {
          transform.position = new Vector3(5, 5, 14);
          wiz.Damage = 0;
        }
        else
        {
          Debug.Log("num players: " + GameObject.FindGameObjectsWithTag("Player").Length);
          // give all other wizardhealthmanager GiveWin()
          foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
          {
            if (!player.GetComponent<NetworkObject>().HasStateAuthority)
            {
              Debug.Log("Given...");
              player.GetComponent<WizardHealthManager>().RPC_GiveWin();
            }
          }

          var network = GameObject.FindGameObjectWithTag("Network");
          var networkManager = network.GetComponent<NetworkDebugStart>();
          networkManager.ShutdownAll();

          // lose life
          PersistencyManager.LoseLifeAndSave();
          Shop.GoToShop();
        }
      }
      else
      {
        wiz.Damage += 0.2f;
      }
    }
  }
}
