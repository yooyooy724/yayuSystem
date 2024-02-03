using UnityEngine;
using yayu.UI;
//namespace Yayu.Utility
//{
    public class YDebugger
    {
        public static void Log(object message) => _Log(message);

    public static void Log(params object[] messages)
    {
        string txt = "";
        for (int i = 0; i < messages.Length; i++)
        {
            if (i > 0) txt += " / ";
            txt += messages[i].ToString();

        }
        _Log(txt);
    }

        private static void _Log(object message)
        {
#if UNITY_EDITOR
        Debug.Log(message);
            //InstantAlert.Alert(message.ToString(), 3.0f);
#endif
        }


        // ---------------------------------------------------------------------------------------
        public static void PopUp(object message) => _PopUp(message);

        public static void PopUp(string label, object message) => _PopUp(label + ": " + message);

        private static void _PopUp(object message)
        {
            InstantAlert.Alert(message.ToString(), 3.0f);
        }

        // ---------------------------------------------------------------------------------------
        public static void Tooltip(object message) => _Tooltip(message);

        public static void Tooltip(string label, object message) => _Tooltip(label + ": " + message);

        private static void _Tooltip(object message)
        {
            InstantAlert.Alert(message.ToString(), 3.0f);
        }


        // ---------------------------------------------------------------------------------------
        public static void LogWarning(object message)
        {
#if UNITY_EDITOR
        Debug.LogWarning(message);
#endif
        }

        public static void LogError(object message)
        {
#if UNITY_EDITOR
        Debug.LogError(message);
        
#endif
    }
    }
//}

