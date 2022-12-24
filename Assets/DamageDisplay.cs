using System;
using UnityEngine;

public class DamageDisplay : MonoBehaviour {
  public Wizard wizard;

  // Update is called once per frame
  void Update() {
    try {

      GetComponent<TMPro.TextMeshPro>().text = wizard.Damage.ToString() + '%';
      transform.LookAt(Camera.main.transform.position);

      // slowly change the text color to red as it approaches 250
      GetComponent<TMPro.TextMeshPro>().color = Color.Lerp(Color.white, Color.red, wizard.Damage / 170f);
    } catch (InvalidOperationException) {

    }
  }
}
