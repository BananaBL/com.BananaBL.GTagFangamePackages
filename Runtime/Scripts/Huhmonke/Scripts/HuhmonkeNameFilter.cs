using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Photon.Pun;

public class HuhmonkeNameFilter : MonoBehaviour
{
    [Header("By HuhMonke")]
    public string[] BadNames;
    public int BanDuration = 48;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(checking());
    }

    IEnumerator checking()
    {
        while(true)
        {
            foreach(string bad in BadNames)
            {
                if (PhotonNetwork.NickName.Contains(bad))
                {
                    Ban();
                }
                yield return new WaitForSeconds(5);
            }
        }
    }

    private void Ban()
    {
        ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest
        {
            FunctionName = "BanPlayer",
            FunctionParameter = new
            {
                DurationInHours = BanDuration,
            },
            GeneratePlayStreamEvent = true
        };
        PlayFabClientAPI.ExecuteCloudScript(request, result =>
        {
            Debug.Log("Worked");
        }, error =>
        {
            Debug.LogError("Error Banning " + error.GenerateErrorReport());
        }
        );
    }
}
