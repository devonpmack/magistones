using Photon.Pun;


public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("test");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public void Start()
    {
        PhotonNetwork.JoinRandomOrCreateRoom(null, 2);
    }
}
