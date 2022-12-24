using UnityEngine;

public class DamageDisplay : MonoBehaviour {
  public Wizard wizard;

  // Update is called once per frame
  void Update() {
    GetComponent<TMPro.TextMeshPro>().text = wizard.Damage.ToString() + '%';
    transform.LookAt(Camera.main.transform.position);
  }
}
