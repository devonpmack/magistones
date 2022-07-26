using Photon.Pun;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject prefab;

    public float x;
    public float y;
    public float z;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(prefab.name, new Vector3(x, y, z), Quaternion.identity);
    }
}
