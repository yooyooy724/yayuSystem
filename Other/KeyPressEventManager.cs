using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyPressEventManager : MonoBehaviour
{
    [System.Serializable]
    public class KeyUnityEventPair
    {
        public KeyCode key;
        public UnityEvent response;
    }

    public List<KeyUnityEventPair> keyEventPairs = new List<KeyUnityEventPair>();

    private void Update()
    {
        foreach (var pair in keyEventPairs)
        {
            if (Input.GetKeyDown(pair.key))
            {
                pair.response.Invoke();
            }
        }
    }
}

