#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class PlayerPrefsTool : EditorWindow
{

    private string PrefsName;

    private float Output;


    [MenuItem("Tools/PlayerPrefs Tool")]
    public static void ShowWindow()
    {
        GetWindow<PlayerPrefsTool>("PlayerPrefs Tool");
    }

    void OnGUI()
    {
        GUILayout.Label("PlayerPrefs Tool", EditorStyles.boldLabel);

        if (GUILayout.Button("Clear Player Prefs"))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs cleared!");
        }
        EditorGUILayout.TextField("Player Prefs Name", PrefsName);
        EditorGUILayout.FloatField("Result",Output);

        if (GUILayout.Button("Get Float"))
        {
            Output = PlayerPrefs.GetFloat(PrefsName);
        }
    }
}
#endif
