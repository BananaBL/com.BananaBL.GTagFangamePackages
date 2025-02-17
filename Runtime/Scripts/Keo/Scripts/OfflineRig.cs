using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OfflineRig : MonoBehaviourPunCallbacks
{
    public GameObject offlineRig;

    void Start()
    {
        offlineRig.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        offlineRig.SetActive(false);
    }

    public override void OnLeftRoom()
    {
        offlineRig.SetActive(true);
    }
}
