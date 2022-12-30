using System;
using System.Linq;
using Fusion;
using UnityEngine;

public class Wizard : NetworkBehaviour {

  [Networked] public int Damage { get; set; }
  [Networked] public TickTimer stun_remaining { get; set; }

  public Ability[] abilities;


  [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.All)]
  public void RPC_Abilities(string[] ownedAbilities) {
    var allAbilities = GetComponents<Ability>();

    var abilityNum = 0;
    foreach (var ownedAbility in ownedAbilities) {
      if (ownedAbility == "None")
        continue;

      abilities[abilityNum++] = allAbilities.First(a => a.GetType().Name.Replace(" ", string.Empty) == ownedAbility.Replace(" ", string.Empty));
    }
  }

  public float damageMultiplier() {
    return (float)(1 + Math.Pow(Damage, 2) / 4000);
  }

  public bool stunned() {
    return !stun_remaining.ExpiredOrNotRunning(Runner);
  }

  public void DoDamage(int damage, Vector3 direction, float stun_duration = 0.4f) {
    GetComponent<NetworkCharacterControllerPrototype>().Velocity = direction.normalized * damage * damageMultiplier();
    Damage += damage;
    stun_remaining = TickTimer.CreateFromSeconds(Runner, stun_duration);
  }

  public override void Spawned() {
    abilities = new Ability[4];

    // gets called each time someone joins, won't work
    if (GetComponent<NetworkCharacterControllerPrototype>().HasInputAuthority && !GetComponent<ControllerPrototype>().bot) {
      GameObject[] display = GameObject.FindGameObjectsWithTag("AbilityIcon");

      var data = PersistencyManager.load();
      RPC_Abilities(data.ownedAbilities.Select(a => a.abilityName).ToArray());

      for (int i = 0; i < abilities.Length; i++) {
        abilities[i].display = display[i].GetComponent<AbilityDisplay>();
      }
    }
  }

  // Update is called once per frame
  void Update() {

  }
}
