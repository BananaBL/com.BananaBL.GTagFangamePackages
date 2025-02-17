using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Photon.VR;

public class OfflineSystem : MonoBehaviourPunCallbacks
{
    public PhotonVRManager PhotonVrManager;
    public TMP_Text OfflineNameText;
    public Renderer[] OfflineColors;


    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        UpdateOffline();
    }

    void UpdateOffline()
    {
        foreach(Renderer rend in OfflineColors)
        {
            rend.material.color = PhotonVrManager.Colour;
        }
        OfflineNameText.text = PhotonNetwork.NickName;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        UpdateOffline();
    }
}
