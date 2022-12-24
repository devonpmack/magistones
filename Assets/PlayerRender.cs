using UnityEngine;

public class PlayerRender : MonoBehaviour {
  // Start is called before the first frame update
  void Start() {
    // randomly change the material color
    GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
  }

  // Update is called once per frame
  void Update() {

  }
}
