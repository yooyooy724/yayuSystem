using Cysharp.Threading.Tasks;
using R3;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace yayu.UI
{
    public class IconedNumberPresenter : MonoBehaviour
    {
        [SerializeField] UITextMono txt;
        [SerializeField] Image image;
        IDisposable disposable;

        public void Init(string spriteName, Func<double> number) => Init(spriteName, number, NumberFormatter.defaultParams);
        public void Init(string spriteName, Func<double> number, NumberFormatter.Params _params)
        {
            InitAsync(spriteName, number, _params).Forget();
        }

        public async UniTask InitAsync(string spriteName, Func<double> number, NumberFormatter.Params _params)
        {
            var sprite = await SpriteManager.GetSpriteAsync(spriteName);
            if (sprite != null) image.sprite = sprite;
            disposable = txt.BindNumberDelegate(number, _params);
        }

        public void InitAsUpdate(string spriteName, Func<double> number, NumberFormatter.Params _params)
        {
            InitAsyncAsUpdate(spriteName, number, _params).Forget();
        }

        public async UniTask InitAsyncAsUpdate(string spriteName, Func<double> number, NumberFormatter.Params _params)
        {
            var sprite = await SpriteManager.GetSpriteAsync(spriteName);
            if (sprite != null) image.sprite = sprite;
            disposable = Observable.EveryUpdate().Subscribe(_ => { txt.SetNumber(number(), _params); });
        }

        public void Dispose()
        {
            disposable?.Dispose();
            image.sprite = null;
        }

        void OnDestroy()
        {
            Dispose();
        }
    }
}