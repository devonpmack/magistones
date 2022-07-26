using Photon.Pun;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public float velocity;
    public float maxDist;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.velocity = transform.forward * velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, new Vector3(0, 0, 11)) > maxDist)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
