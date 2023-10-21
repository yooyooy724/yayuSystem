using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticPopUp : MonoBehaviour
{
    public static StaticPopUp Instance;
    static GameObject _go;
    static TEXT _textField;
    [SerializeField] static TEXT textField() 
    {
        return _textField;
    }
    static List<string> texts = new List<string>();

    void Awake()
    {
        _go = gameObject;
        _textField = _go.GetComponentInChildren<TEXT>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        gameObject.SetActive(false);
    }

    public static void ShowPopup(string text)
    {
        texts.Add(text);
        textField().SetText(PupUpText());
        Instance.gameObject.SetActive(true);
    }

    static string PupUpText()
    {
        string txt = "";
        for (int i = 0; i < texts.Count; i++)
        {
            txt += texts[i];
            if(i != texts.Count - 1) txt += "\n";
        }
        return txt;
    }

    public static void HidePopup()
    {
        Instance.gameObject.SetActive(false);
    }
}
