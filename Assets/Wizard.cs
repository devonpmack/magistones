using System;
using Fusion;
using UnityEngine;

public class Wizard : NetworkBehaviour {

  [Networked] public int Damage { get; set; }
  [Networked] public TickTimer stun_remaining { get; set; }

  public Ability primary;

  public float damageMultiplier() {
    return (float)(1 + Math.Pow(Damage, 2) / 4000);
  }

  public bool stunned() {
    return !stun_remaining.ExpiredOrNotRunning(Runner);
  }


  // Start is called before the first frame update
  void Start() {
    if (GetComponent<NetworkCharacterControllerPrototype>().HasInputAuthority) {
      AbilityDisplay display = GameObject.FindGameObjectWithTag("AbilityIcon").GetComponent<AbilityDisplay>();
      display.ability = primary;
    }
  }

  // Update is called once per frame
  void Update() {

  }
}
