using UnityEngine;
using yayu.ui;
//namespace Yayu.Utility
//{
    public class Debuger
    {
        public static void Log(object message) => _Log(message);

        public static void Log(string label, object message) => _Log(label + ": " + message);

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

