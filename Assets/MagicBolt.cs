using Photon.Pun;
using UnityEngine;

public class MagicBolt : Ability
{
    public GameObject laser;
    public override void onCast(Transform player)
    {
        PhotonNetwork.Instantiate("Abilities/" + laser.name, player.position, player.rotation);
    }
}
