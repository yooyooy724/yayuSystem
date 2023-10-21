using Cysharp.Threading.Tasks;
using Doozy.Runtime.UIManager.Containers;
using UniRx;
using UnityEngine;

namespace yayu.ui
{
    public class InstantAlert : MonoBehaviour
    {
        [SerializeField] TEXT popUpText;
        [SerializeField] CanvasGroup canvasGroup;
        [SerializeField] UIContainer canvas;
        [SerializeField] bool isDebug = false;

        // Ensure this is a singleton.
        public static InstantAlert instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            canvasGroup.alpha = 0; 
        }

        private void Start()
        {
            if (isDebug)
            {
                Observable.TimerFrame(120).Subscribe(_ => Alert("InstantAlert is working!", 3.0f));
            }
        }

        public static void Alert(string message, float displayTime)
        {
            if (instance == null) return;
            _ = instance.DisplayPopUp(message, displayTime); // Discard the UniTask. 
        }

        private async UniTaskVoid DisplayPopUp(string message, float displayTime)
        {
            popUpText.SetText(message);
            canvasGroup.alpha = 1; // Make sure text is fully opaque
            canvas.Show();

            float elapsed = 0.0f;
            while (elapsed < displayTime)
            {
                float alpha = Mathf.Lerp(1, 0, elapsed / displayTime); // Linearly interpolate alpha value from 1 to 0
                canvasGroup.alpha = alpha;
                elapsed += Time.deltaTime;

                await UniTask.Yield(); // Await until next frame
            }
            canvas.Hide();
        }
    }
}
