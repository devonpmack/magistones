using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
public class AbilityCaster : MonoBehaviour
{
    PhotonView view;
    public GameObject ability;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && view.IsMine)
        {
            onCast();
        }

    }

    private void onCast()
    {
        PhotonNetwork.Instantiate(ability.name, transform.position, transform.rotation);
    }
}
