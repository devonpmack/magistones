using System;
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
    if (GetComponent<NetworkCharacterControllerPrototype>().HasInputAuthority && !GetComponent<ControllerPrototype>().bot) {
      GameObject[] display = GameObject.FindGameObjectsWithTag("AbilityIcon");
      abilities = GetComponents<Ability>();

      for (int i = 0; i < abilities.Length; i++) {
        abilities[i].display = display[i].GetComponent<AbilityDisplay>();
      }
    }
  }

  // Update is called once per frame
  void Update() {

  }
}
