using Photon.Pun;
using UnityEngine;
using System.Collections.Generic;
public class CameraConflictingMeshFixer : MonoBehaviour
{
    public PhotonView view;
    public List<Renderer> ConflictingMeshes = new List<Renderer>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (!Application.isEditor)
        {
            foreach (Renderer rend in ConflictingMeshes)
            {
                if (view.IsMine)
                {
                    rend.gameObject.SetActive(false);
                }
                else
                {
                    rend.gameObject.SetActive(true);
                }
            }
        }
    }
}
