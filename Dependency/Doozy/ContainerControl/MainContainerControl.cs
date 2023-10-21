using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using UniRx;

public class MainContainerControl : MonoBehaviour
{
    [SerializeField] UIView main;
    [SerializeField] UIToggleGroup toggleGroup;
    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < toggleGroup.toggles.Count; i++)
        //{
        //    var count = i;
        //    toggleGroup.toggles
        //}
    }

    // Update is called once per frame
    void UpdateMainView()
    {
        var toggles = toggleGroup.toggles;
        for (int i = 0; i < toggles.Count; i++)
        {
            if (toggles[i].isOn)
            {
                if(main.isVisible)
                    main.Hide();
                return;
            }
        }
        main.Show();
    }
}
