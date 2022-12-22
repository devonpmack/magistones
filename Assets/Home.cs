using UnityEngine;

public class Home : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public static void OnClick()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
  }
}
