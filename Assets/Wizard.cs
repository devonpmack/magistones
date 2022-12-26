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


  // Start is called before the first frame update
  void Start() {
    if (GetComponent<NetworkCharacterControllerPrototype>().HasInputAuthority) {
      GameObject[] display = GameObject.FindGameObjectsWithTag("AbilityIcon");
      display[0].GetComponent<AbilityDisplay>().ability = abilities[0];
      display[1].GetComponent<AbilityDisplay>().ability = abilities[1];
      display[2].GetComponent<AbilityDisplay>().ability = abilities[2];
      display[3].GetComponent<AbilityDisplay>().ability = abilities[3];

    }
  }

  // Update is called once per frame
  void Update() {

  }
}
