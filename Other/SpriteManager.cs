using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
    static SpriteManager Instance { get; set; }
    public static bool IsInitialized { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            IsInitialized = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static async UniTask<Sprite> GetSpriteAsync(string spriteName)
    {
        await UniTask.WaitUntil(() => IsInitialized);

        ResourceRequest resourceRequest = Resources.LoadAsync<Sprite>("Sprites/" + spriteName);
        await UniTask.WaitUntil(() => resourceRequest.isDone);
        Sprite sprite = resourceRequest.asset as Sprite;
        if (sprite == null)
        {
            Debug.LogWarning("Sprite not found: " + spriteName);
        }
        return sprite;
    }

    public static Sprite GetSprite(string spriteName)
    {
        Sprite sprite = Resources.Load<Sprite>("Sprites/" + spriteName);
        if (sprite == null)
        {
            Debug.LogWarning("Sprite not found: " + spriteName);
        }
        return sprite;
    }
}