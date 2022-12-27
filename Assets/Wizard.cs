using System;
using System.Linq;
using Fusion;
using UnityEngine;

public class Wizard : NetworkBehaviour {

  [Networked] public int Damage { get; set; }
  [Networked] public TickTimer stun_remaining { get; set; }

  public Ability[] abilities;

  public float damageMultiplier() {
    return (float)(1 + Math.Pow(Damage, 2) / 4000);
  }

  public bool stunned() {
    return !stun_remaining.ExpiredOrNotRunning(Runner);
  }

  public override void Spawned() {
    abilities = new Ability[4];

    // gets called each time someone joins, won't work
    if (GetComponent<NetworkCharacterControllerPrototype>().HasInputAuthority && !GetComponent<ControllerPrototype>().bot) {
      GameObject[] display = GameObject.FindGameObjectsWithTag("AbilityIcon");
      var allAbilities = GetComponents<Ability>();

      var data = PersistencyManager.load();
      var abilityNum = 0;
      if (data.HasValue) {
        foreach (var ownedAbility in data.Value.ownedAbilities) {
          Debug.Log(ownedAbility.abilityName);
          abilities[abilityNum++] = allAbilities.First(a => a.GetType().Name.Replace(" ", string.Empty) == ownedAbility.abilityName.Replace(" ", string.Empty));
        }
      }

      for (int i = 0; i < abilities.Length; i++) {
        abilities[i].display = display[i].GetComponent<AbilityDisplay>();
      }
    }
  }

  // Update is called once per frame
  void Update() {

  }
}
