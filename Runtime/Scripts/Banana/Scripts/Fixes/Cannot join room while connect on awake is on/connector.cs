using UnityEngine;
using Photon.VR;
public class connector : MonoBehaviour
{
    public PhotonVRManager manager;
    void Start()
    {
        manager.JoinRoomOnConnect = false;
        pp();
    }

    void pp()
    {
        manager.JoinRoomOnConnect = true;
    }
}
