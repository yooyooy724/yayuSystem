using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yayu.ui;

public class TestDescriptionTaker : MonoBehaviour, IDescriptionTaker
{
    private void Start()
    {
        id = System.Guid.NewGuid().ToString();
    }
    string id;
    public object Description()
    {
        string txt = "";
        txt += "Time is" + Time.time.ToString("F2") + "sec \n";
        txt += "FPS is" + (1 / Time.deltaTime).ToString("F5") + "fps \n";
        return txt;
    }

    public object Footer()
    {
        return "desc id is " + id;
    }

    public object Header()
    {
        return "PlayMode Timer";
    }
}
