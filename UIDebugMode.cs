using UnityEngine;

namespace yayu.UI
{
    public class UIDebugMode : MonoBehaviour
    {
        [SerializeField] UIToggleMono toggle;
        public static bool isDebugMode = false;
        void Start()
        {
            toggle.AddListener_OnValueChanged(_ => isDebugMode = _);
        }
    }
}
