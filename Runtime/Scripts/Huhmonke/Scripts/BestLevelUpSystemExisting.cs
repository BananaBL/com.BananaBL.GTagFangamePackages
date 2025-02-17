using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BestLevelUpSystemExisting : MonoBehaviour
{

    [System.Serializable]
    public struct LevelSettings
    {
        [Header("Reward Name")]
        public string Name;
        public string Description;

        [Header("Reward Type")]
        public bool GiveCurreny;
        public bool EnableGameobject;
        public bool DisableGameobject;
        public bool SpawnObject;

        [Header("Reward")]
        public GameObject ObjectToSpawn;
        public Transform SpawnPos;
        public GameObject[] DisableObject;
        public GameObject[] EnableObject;
        public int CurrenyAmount;

        [Header("Level To Unlock")]
        public int LevelToUnlock;
    }
    
    
    [Header("By HuhMonke Give Credits")]

    [Header("Text To show Level")]
    public TMPro.TMP_Text LevelText;
    public TMPro.TMP_Text RewardText;
    public TMPro.TMP_Text DescriptionText;

    [Header("Currency Code")]
    public string CurrenyCode = "HS";

    [Header("Reward System")]
    public LevelSettings[] LevelSystem;

    [Header("Xp Settings")]
    public float XpPerSec = 0.01f;
    public int XpAmountToLevelUp = 100;
    public int XpItWillAddEveryTimeULevelUp = 50;

    //Privates
    [HideInInspector] public int CurrentLevel = 0;
    [HideInInspector] public float CurrentXp = 0.0f;

    private void Start()
    {
        LoadPrefs();
        InvokeRepeating("GetXP", 1.0f, 1.0f);
    }

    void GetXP()
    {
        CurrentXp += XpPerSec;
        if(CurrentXp >= XpAmountToLevelUp)
        {
            levelUp();
        }
        foreach(var rewards in LevelSystem)
        {
            if(RewardText != null)
            {
                RewardText.text = "Next Reward: " + rewards.Name.ToString() + " Unlocks at level: " + rewards.LevelToUnlock;
            }
            if(DescriptionText != null)
            {
                DescriptionText.text = rewards.Description.ToString();
            }

            if (CurrentLevel == rewards.LevelToUnlock)
            {
                if(rewards.GiveCurreny)
                {
                    GetMoney(rewards.CurrenyAmount);
                }
                if (rewards.EnableGameobject)
                {
                    foreach(var obj in rewards.EnableObject)
                    {
                        obj.SetActive(true);
                    }
                }
                if (rewards.DisableGameobject)
                {
                    foreach (var obj in rewards.DisableObject)
                    {
                        obj.SetActive(false);
                    }
                }
                if (rewards.SpawnObject)
                {
                    Instantiate(rewards.ObjectToSpawn, rewards.SpawnPos);
                }
            }
        }
        if(LevelText != null)
        {
            LevelText.text = "Level: " + CurrentLevel + " XP: " + CurrentXp + " / " + XpAmountToLevelUp;
        }
        SavePrefs();
    }

    void GetMoney(int Amount)
    {
        var request = new PlayFab.ClientModels.AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = CurrenyCode,
            Amount = Amount
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnSucess, OnError);
    }

    void OnSucess(PlayFab.ClientModels.ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("Currency Successfully");
    }

    public void OnGetInventorySuccess(PlayFab.ClientModels.GetUserInventoryResult result)
    {
        int currency = result.VirtualCurrency[CurrenyCode];
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error");
    }

    void levelUp()
    {
        CurrentLevel++;
        CurrentXp = 0.0f;
        XpAmountToLevelUp += XpItWillAddEveryTimeULevelUp;
        SavePrefs();
    }

    void SavePrefs()
    {
        PlayerPrefs.SetInt("currentLevel", CurrentLevel);
        PlayerPrefs.SetFloat("currentXP", CurrentXp);
        PlayerPrefs.SetInt("xPRequiredForLevelUp", XpAmountToLevelUp);
        PlayerPrefs.Save();
    }

    void LoadPrefs()
    {
        CurrentLevel = PlayerPrefs.GetInt("currentLevel", CurrentLevel);
        CurrentXp = PlayerPrefs.GetFloat("currentXP", CurrentXp);
        XpAmountToLevelUp = PlayerPrefs.GetInt("xPRequiredForLevelUp", XpAmountToLevelUp);
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(BestLevelUpSystemExisting))]
public class BestLevelUpSystemExistingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BestLevelUpSystemExisting level = (BestLevelUpSystemExisting)target;
        GUILayout.Space(10);
        GUILayout.Label("Level Buttons", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Reset Level And XP"))
        {
            level.CurrentLevel = 0;
            level.CurrentXp = 0.0f;
        }
        if(GUILayout.Button("Add 5 Levels"))
        {
            level.CurrentLevel += 5;
        }
        if (GUILayout.Button("Add 10 Levels"))
        {
            level.CurrentLevel += 10;
        }
        if (GUILayout.Button("Add 15 Levels"))
        {
            level.CurrentLevel += 15;
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(5);
        GUILayout.Label("CurrentXP: " + level.CurrentXp, EditorStyles.boldLabel);
        GUILayout.Label("CurrentLevel: " + level.CurrentLevel, EditorStyles.boldLabel);

        base.OnInspectorGUI();
    }
}
#endif