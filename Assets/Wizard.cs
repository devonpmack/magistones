using System;
using Fusion;

public class Wizard : NetworkBehaviour {

  [Networked] public int Damage { get; set; }

  public float damageMultiplier() {
    return (float)(1 + Math.Pow(Damage, 2) / 2000);
  }


  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {

  }
}
