using Cinemachine;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject prefab;
    public GameObject cameraFollow;

    public CinemachineVirtualCamera cam;

    public float x;
    public float y;
    public float z;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject obj = PhotonNetwork.Instantiate(prefab.name, new Vector3(x, y, z), Quaternion.identity);

        //cam.m_Follow = obj.GetComponent<Transform>();



        //cam.m_LookAt = obj.GetComponent<Transform>();
    }
}
