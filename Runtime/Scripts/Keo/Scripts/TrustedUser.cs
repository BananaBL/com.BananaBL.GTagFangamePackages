using UnityEngine;
using TMPro;

public class TrustedUser : MonoBehaviour
{

    [Header("This script was made by Keo.cs")]
    [Header("You do not have to give credits")]
    public GameObject objectToToggle;
    public TextMeshPro countdownText;
    public int StartingTime = 1000;
    public bool EnableObject;

    private float countdownTime;
    private bool isTimerActive = true;

    private void Start()
    {
        countdownTime = PlayerPrefs.GetFloat("", StartingTime);
    }

    private void Update()
    {
        if (isTimerActive)
        {
            countdownTime -= Time.deltaTime;

            if (countdownTime <= 0)
            {
                if (objectToDisable != null)
                {
                    objectToToggle.SetActive(EnableObject);
                    PlayerPrefs.SetInt("ObjectDisabled", 1);
                }

                countdownTime = 0;
                isTimerActive = false;
            }

            PlayerPrefs.SetFloat("", countdownTime);

            if (countdownText != null)
            {
                int displayTime = Mathf.CeilToInt(countdownTime);
                countdownText.text = "" + displayTime.ToString();
            }
        }
    }
}
