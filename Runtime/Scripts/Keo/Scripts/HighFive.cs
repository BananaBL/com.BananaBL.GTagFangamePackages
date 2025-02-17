using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]

public class HighFive : MonoBehaviour
{
    public GameObject HighFivePrefab;
    public float DespawnDelay;
    public float CoolDown;
    public AudioSource AudioPlayer;
    public List<AudioClip> Sounds;
    public bool CanClapSelf;

    private PhotonView PTView;
    private bool CanDo = true;
    private GameObject CurrentParticle;
    private bool IsTheChoosenOne;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HighFive>() != null && CanDo)
        {
            if (other.GetComponent<HighFive>().IsTheChoosenOne != true)
            {
                if (!CanClapSelf && !other.GetComponent<PhotonView>().IsMine)
                {
                    StartCoroutine(HighFiveFunktion());
                    PTView.RPC(nameof(HighFiveButLol), RpcTarget.All);
                }
                else if (CanClapSelf)
                {
                    StartCoroutine(HighFiveFunktion());
                    PTView.RPC(nameof(HighFiveButLol), RpcTarget.All);
                }
            }
        }
    }

    public IEnumerator HighFiveFunktion()
    {
        CanDo = false;
        IsTheChoosenOne = true;
        //Idk what happend there i belive clicked tab to many times but im lazy so i leave it there if that alright
        yield    return new WaitForSeconds(CoolDown);
        CanDo = true;
        IsTheChoosenOne = false;
    }

    [PunRPC]
    public void HighFiveButLol()
    {
        //Note to keo add sound cause i gota do sync now see you later futer Keo

        //Im back and now im adding sounds
        if (Sounds.Count != 0)
        {
            int RIDX = Random.Range(0, Sounds.Count);
            AudioPlayer.clip = Sounds[RIDX];
            AudioPlayer.Play();
        }
        CurrentParticle = Instantiate(HighFivePrefab);
        new WaitForSeconds(DespawnDelay);
        Destroy(CurrentParticle);
    }
}
