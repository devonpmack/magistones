using Fusion;
using UnityEngine;

public class Dash : Ability
{
  public float dashDistance = 3f;

  [Networked] private TickTimer dash_remaining { get; set; }


  protected override void onCast(NetworkInputPrototype input)
  {
    transform.LookAt(new Vector3(input.mouse_x, transform.position.y, input.mouse_z));
    GetComponent<NetworkCharacterControllerPrototype>().Velocity = transform.forward * dashDistance * 10;
    GetComponent<Wizard>().stun_remaining = TickTimer.CreateFromSeconds(Runner, 0.3f);
    dash_remaining = TickTimer.CreateFromSeconds(Runner, 0.5f);
  }

  public override void FixedUpdateNetwork()
  {
    base.FixedUpdateNetwork();

    if (!dash_remaining.ExpiredOrNotRunning(Runner))
    {
      // check if there any any nearby players
      var players = GameObject.FindGameObjectsWithTag("Player");
      foreach (var player in players)
      {

        if (player != gameObject && Vector3.Distance(player.transform.position, transform.position) < 1.5f && !player.GetComponent<Wizard>().stunned())
        {
          player.GetComponent<Wizard>().RPC_DoDamage(20, transform.forward);
        }
      }
    }
  }
}
