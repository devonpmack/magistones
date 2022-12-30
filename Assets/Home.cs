using UnityEngine;

public class Home : MonoBehaviour {
  public GameObject homepage;
  public GameObject roll;

  public static void GoToGame() {
    UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
  }

  public void GoToRoll() {
    homepage.SetActive(false);
    roll.SetActive(true);
  }

  public void GoToHome() {
    homepage.SetActive(true);
    roll.SetActive(false);
  }
}
