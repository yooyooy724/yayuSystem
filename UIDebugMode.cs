using UnityEngine;

namespace yayu.UI
{
    public class UIDebugMode : MonoBehaviour
    {
        [SerializeField] UIToggleMono toggle;
        public static bool isDebugMode = false;
        void Start()
        {
            Toggle tgl = new Toggle("");
            tgl.AddListener_OnValueChanged(_ => isDebugMode = _);
            UIElementConnection.ConnectToggle(tgl, toggle);
        }
    }
}
