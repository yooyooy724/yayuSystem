using UnityEngine;

namespace My.UI
{
    public class testText : MonoBehaviour
    {
        [SerializeField] private UITextMono textPresenter;
        private float deltaTime = 0.0f;

        private void Update()
        {
            if (Time.frameCount % 10 == 0)
            {
                deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f; // Smooth the delta time calculation
                float fps = 1.0f / deltaTime;

                textPresenter.SetText($"FPS: {Mathf.CeilToInt(fps)}");
            }
        }
    }
}