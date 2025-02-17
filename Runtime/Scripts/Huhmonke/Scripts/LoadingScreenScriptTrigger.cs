using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenScript : MonoBehaviour
{
    public GameObject[] LoadingScreen,MainMenu;

    public Slider LoadingScreenSlider;
    public TMP_Text LoadingScreenText;

    public bool OnTrigger;
    public string SceneName;
    public string HandTag = "HandTag";

    private void Awake()
    {
        if(LoadingScreenSlider != null)
        {
            LoadingScreenSlider.maxValue = 1;
            LoadingScreenSlider.minValue = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(OnTrigger && other.CompareTag(HandTag))
        {
            LoadScene(SceneName);
        }
    }

    public void LoadScene(string SceneName)
    {
        foreach(GameObject obj in MainMenu)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
        foreach (GameObject obj in LoadingScreen)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
        StartCoroutine(LoadSceneASync(SceneName));
    }

    IEnumerator LoadSceneASync(string LevelName)
    {
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(LevelName);
        while(!loadOp.isDone)
        {
            float ProgressValue = Mathf.Clamp01(loadOp.progress / 0.9f);
            if(LoadingScreenSlider != null)
            {
                LoadingScreenSlider.value = ProgressValue;
            }
            if(LoadingScreenText != null)
            {
                LoadingScreenText.text = $"{(ProgressValue * 100):0}%";
            }
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
