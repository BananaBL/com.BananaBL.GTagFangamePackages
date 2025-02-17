using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using easyInputs;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(AudioSource))]
public class AnimalCompanyHitsounds : MonoBehaviourPun
{
    [System.Serializable]
    public struct Sounds
    {
        public string HitSoundName;
        public AudioClip[] HitSounds;
        public string tag;
    }

    [Header("Made By HuhMonke")]
    [Header("Give Credits Discord: https://discord.gg/MMMsvTWBZa")]
    [Header("And Dont Claim As Ur Own")]

    [Header("The HitSounds")]
    public Sounds[] HitSounds;

    [Header("The Hitsounds")]
    public AudioSource aud;

    [Header("Auto Settings")]
    public bool UseAutoSettings = false;

    [Header("Should Network?")]
    public bool Networking;

    [Header("Vibrations Requires EasyInputs")]
    public EasyHand Hand;
    public float VibrationAmount = 0.15f;

    [Header("Pitch Settings")]
    public float MinPitch = 0.8f;
    public float MaxPitch = 4;

    [Header("Use Unity Editor?")]
    public bool UseUnityEditor = true;

    [HideInInspector] public bool Touching = false;

    // Start is called before the first frame update
    void Start()
    {
        if (aud != null)
        {
            if(UseAutoSettings)
            {
                aud.spatialBlend = 1;
                aud.rolloffMode = AudioRolloffMode.Linear;
                aud.minDistance = 5;
                aud.maxDistance = 10;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var sound in HitSounds)
        {
            if (other.CompareTag(sound.tag))
            {
                if (!Touching)
                {
                    int randomNumber = Random.Range(0, sound.HitSounds.Length);

                    aud.clip = sound.HitSounds[randomNumber];

                    aud.pitch = Random.Range(MinPitch, MaxPitch);

                    PlayAudio();
                    
                    if (Networking && PhotonNetwork.InRoom)
                    {
                        photonView.RPC("PlayAudio", RpcTarget.Others);
                    }

                    StartCoroutine(EasyInputs.Vibration(Hand, VibrationAmount, 0.15f));
                    Touching = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (var sound in HitSounds)
        {
            if (other.CompareTag(sound.tag))
            {
                if (Touching)
                {
                    Touching = false;
                }
            }
        }
    }

    [PunRPC]
    public void PlayAudio()
    {
        aud.Play();
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(AnimalCompanyHitsounds))]
public class AnimalCompanyHitsoundsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        AnimalCompanyHitsounds sigma = (AnimalCompanyHitsounds)target;

        
        if(sigma.UseUnityEditor)
        {
            GUILayout.Space(5);
            GUILayout.Label("Hitting Ground: " + sigma.Touching, EditorStyles.boldLabel);
            if(sigma.aud != null)
            {
                GUILayout.Label("Current Pitch: " + sigma.aud.pitch, EditorStyles.boldLabel);
            }
        }
    }
}
#endif