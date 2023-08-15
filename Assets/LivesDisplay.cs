using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesDisplay : MonoBehaviour
{

  public GameObject heart;
  public GameObject emptyHeart;
  // Start is called before the first frame update
  void Start()
  {
    // clear children
    foreach (Transform child in transform)
    {
      GameObject.Destroy(child.gameObject);
    }


    var data = PersistencyManager.load();

    for (int i = 0; i < data.lives; i++)
    {
      Instantiate(heart, transform);
    }

    for (int i = 0; i < PersistencyManager.MAX_LIVES - data.lives; i++)
    {
      Instantiate(emptyHeart, transform);
    }
  }

}
