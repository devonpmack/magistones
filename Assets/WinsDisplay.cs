using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinsDisplay : MonoBehaviour
{

  public GameObject star;
  public GameObject emptyStar;
  // Start is called before the first frame update
  void Start()
  {
    // clear children
    foreach (Transform child in transform)
    {
      GameObject.Destroy(child.gameObject);
    }


    var data = PersistencyManager.load();

    for (int i = 0; i < data.wins; i++)
    {
      Instantiate(star, transform);
    }

    for (int i = 0; i < PersistencyManager.MAX_WINS - data.wins; i++)
    {
      Instantiate(emptyStar, transform);
    }
  }

}
