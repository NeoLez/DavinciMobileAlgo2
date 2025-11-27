using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public enum Languages
{
    Spanish,
    English
}

public class Localization : MonoBehaviour {
    public static Localization Ins;
    [SerializeField] string _webUrl = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRVTI--PlgIhhTWFqtQn38Yq6uK9s9ZMcDmzFWm80Q668NRfDBIvecouSOJDA_STK_djaqpBzs08Hx_/pub?gid=0&single=true&output=csv";
    [SerializeField] private bool update;
    Dictionary<Languages, Dictionary<string, string>> _localization;

    [SerializeField] Languages _currentLanguage;

    public event Action OnUpdate;

    private void Update() {
        if (update) {
            update = false;
            OnUpdate?.Invoke();
        }
    }

    private void Awake() {
        Ins = this;
        StartCoroutine(DownloadCSV(_webUrl));
        DontDestroyOnLoad(this);
    }

    IEnumerator DownloadCSV(string url)
    {
        var www = new UnityWebRequest(url);

        www.downloadHandler = new DownloadHandlerBuffer();

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Web Download Success");

            var split = new LanguageSplit();

            _localization = split.LoadCSV(www.downloadHandler.text, "web");

            SaveText(fileName: "Codex", content: www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Web Download Failed");

            var result = LoadText("Codex");

            var split = new LanguageSplit();
            _localization = split.LoadCSV(result, "hard disk");
        }

        OnUpdate?.Invoke();
    }

    public string GetTranslate(string ID)
    {
        var idsDictionary = _localization[_currentLanguage];

        idsDictionary.TryGetValue(ID, out var result);

        return result;
    }

    void SaveText(string fileName, string content)
    {
        string path = Application.persistentDataPath + "/" + fileName;

        try
        {
            File.WriteAllText(path, content);
            Debug.Log("File saved successfully at: " + path);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save file: " + e.Message);
        }
    }

    string LoadText(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName;

        try
        {
            if (File.Exists(path))
            {
                string content = File.ReadAllText(path);
                Debug.Log("File loaded successfully from: " + path);
                return content;
            }
            else
            {
                Debug.LogWarning("File not found at: " + path);
                return null;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to load file: " + e.Message);
            return null;
        }
    }
}
