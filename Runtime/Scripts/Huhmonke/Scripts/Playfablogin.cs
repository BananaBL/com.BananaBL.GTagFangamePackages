using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using System.IO;
using UnityEngine.Networking;

public class Playfablogin : MonoBehaviour
{
    [Header("COSMETICS")]
    public static Playfablogin instance;
    public string MyPlayFabID;
    public string CatalogName;
    public List<GameObject> specialitems;
    public List<GameObject> disableitems;
    [Header("CURRENCY")]
    public string CurrencyName;
    public string VirtualCurrency = "HS";
    public TextMeshPro currencyText;
    [SerializeField]
    public int coins;
    [Header("Info Text")]
    public TMP_Text LoggingInText;
    [Header("BANNED")]
    public bool KickOnBan = true;
    public bool DisconnectPhotonOnBan = true;
    public bool LoadSceneOnBan = false;
    public bool TeleportOnBan = false;
    public Transform Player;
    public Transform OnBanTeleportTo;
    public GameObject[] ObjEnableOnBan;
    public GameObject[] ObjDisableOnBan;
    public string bannedscenename;
    [Header("Cheater")]
    public string Webhook = "";
    public string[] Dlls;
    [Header("TITLE DATA")]
    public TextMeshPro MOTDText;
    [Header("PLAYER DATA")]
    public TextMeshPro UserName;
    public string StartingUsername;
    public string name;
    [SerializeField]
    public bool UpdateName;

    private bool CanSend = true;




    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        login();
        InvokeRepeating(nameof(CheckVar), 20, 20);
    }

    public void login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        if(LoggingInText != null)
        {
            LoggingInText.text = "Logging In";
        }
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }

    public void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("logging in");
        GetAccountInfoRequest InfoRequest = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(InfoRequest, AccountInfoSuccess, OnError);
        GetVirtualCurrencies();
        GetMOTD();
        CheckForBan();
        if (LoggingInText != null)
        {
            LoggingInText.text = "Logged in";
            new WaitForSeconds(1);
            LoggingInText.text = "";
        }
    }

    public void AccountInfoSuccess(GetAccountInfoResult result)
    {
        MyPlayFabID = result.AccountInfo.PlayFabId;

        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(),
        (result) =>
        {
            foreach (var item in result.Inventory)
            {
                if (item.CatalogVersion == CatalogName)
                {
                    for (int i = 0; i < specialitems.Count; i++)
                    {
                        if (specialitems[i].name == item.ItemId)
                        {
                            specialitems[i].SetActive(true);
                        }
                    }
                    for (int i = 0; i < disableitems.Count; i++)
                    {
                        if (disableitems[i].name == item.ItemId)
                        {
                            disableitems[i].SetActive(false);
                        }
                    }
                }
            }
        },
        (error) =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }

    async void Update()
    {
    }

    public void GetVirtualCurrencies()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnError);
    }

    void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        coins = result.VirtualCurrency[VirtualCurrency];
        currencyText.text = "You have " + coins.ToString() + " " + CurrencyName;
    }

    private void OnError(PlayFabError error)
    {
        if (error.Error == PlayFabErrorCode.AccountBanned)
        {
            badstuff();
        }
        if(error.Error == PlayFabErrorCode.AccountNotFound || error.Error == PlayFabErrorCode.AccountDeleted)
        {
            if (LoggingInText != null)
            {
                LoggingInText.text = "Error Retrying";
            }
            login();
        }
    }

    void badstuff()
    {
        if (DisconnectPhotonOnBan)
        {
            PhotonNetwork.Disconnect();
        }
        if (TeleportOnBan)
        {
            StartCoroutine(Teleport());
        }
        if (KickOnBan)
        {
            Application.Quit();
        }
        if (LoadSceneOnBan)
        {
            SceneManager.LoadScene(bannedscenename);
        }
    }


    /// <summary>
    /// Call this function to detect files for hacks -HuhMonke
    /// </summary>
    public void CheckVar()
    {
        if(CanSend)
        {
            string msgg = PhotonNetwork.NickName + " Is Modding PlayfabID: " + MyPlayFabID;
            
            string Pathh = "/storage/emulated/0/Android/data/" + Application.identifier + "/files/Mods";
            string lemon = Path.Combine(Application.persistentDataPath, "lemonloader");
            string melon = Path.Combine(Application.persistentDataPath, "melonloader");
            string directory = Directory.GetCurrentDirectory();

            if(Directory.Exists(Pathh))
            {
                badstuff();
                StartCoroutine(Webhook, msgg);
                CanSend = false;
            }
            if (Directory.Exists(lemon))
            {
                badstuff();
                StartCoroutine(Webhook, msgg);
                CanSend = false;
            }
            if (Directory.Exists(melon))
            {
                badstuff();
                StartCoroutine(Webhook, msgg);
                CanSend = false;
            }
            foreach(var dll in Dlls)
            {
                if(File.Exists(Path.Combine(directory, dll)))
                {
                    badstuff();
                    StartCoroutine(Webhook, msgg);
                    CanSend = false;
                }
            }
        }
    }

    IEnumerator webhooking(string link, string msg)
    {
        WWWForm form = new WWWForm();
        form.AddField("content", msg);
        using (UnityWebRequest www = UnityWebRequest.Post(link, form))
        {
            yield return www.SendWebRequest();
        }
    }

    IEnumerator Teleport()
    {
        Collider[] colliderr = FindObjectsOfType<Collider>();
        foreach(Collider col in colliderr)
        {
            col.gameObject.SetActive(false);
        }
        Player.position = OnBanTeleportTo.position;
        yield return new WaitForSeconds(0.1f);
        foreach (Collider col in colliderr)
        {
            col.gameObject.SetActive(true);
        }

        while(true)
        {
            Player.position = OnBanTeleportTo.position;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator CheckForBan()
    {
        while(true)
        {
            yield return new WaitForSeconds(60);
            Checking();
        }
    }

    void Checking()
    {
        var request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, AccountInfoSuccess, OnError);
    }

    public void GetMOTD()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), MOTDGot, OnError);
    }

    public void MOTDGot(GetTitleDataResult result)
    {
        if (result.Data == null || result.Data.ContainsKey("MOTD") == false)
        {
            Debug.Log("No MOTD");
            return;
        }
        MOTDText.text = result.Data["MOTD"];

    }


}