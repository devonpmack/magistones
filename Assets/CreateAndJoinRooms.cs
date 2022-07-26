using Photon.Pun;


public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public void CreateRoom()
    {

        PhotonNetwork.CreateRoom("test");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("test");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
