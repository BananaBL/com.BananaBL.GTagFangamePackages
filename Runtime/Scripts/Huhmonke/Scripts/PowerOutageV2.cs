using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class PowerOutageV2 : MonoBehaviourPun
{
    [Header("Made By HuhMonke !MAKE SURE TO PUT THIS SCRIPT ON AN EMPTYGAMEOBJECT!")]
    [Header("Max and Min Time Before it turns off")]
    public float MaxTime = 100;
    public float MinTime = 90;
    [Header("Every Object To Enable And Disable When Doing Power Outage")]
    public GameObject[] Disables;
    public GameObject[] Enables;
    [Header("Collider To Trigger Reset")]
    public Collider Collider;
    public string TagToReset = "HandTag";

    private bool Counting = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("CheckIfMaster", 15, 5);
    }

    // Update is called once per frame
    void CheckIfMaster()
    {
        if(PhotonNetwork.IsMasterClient && !Counting && PhotonNetwork.InRoom)
        {
            StartCoroutine(StartCountdown());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(TagToReset) && Counting)
        {
            StopCoroutine(StartCountdown());
            photonView.RPC("DisableEverythingForPeople", RpcTarget.All);
            Counting = false;
        }
    }

    [PunRPC]
    void EnableEverythingForPeople()
    {
        foreach (GameObject obj in Enables)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in Disables)
        {
            obj.SetActive(false);
        }
    }

    [PunRPC]
    void DisableEverythingForPeople()
    {
        foreach (GameObject obj in Enables)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in Disables)
        {
            obj.SetActive(true);
        }
    }

    IEnumerator StartCountdown()
    {
        Counting = true;
        yield return new WaitForSeconds(Random.Range(MinTime, MaxTime));
        photonView.RPC("EnableEverythingForPeople", RpcTarget.All);
    }

}
