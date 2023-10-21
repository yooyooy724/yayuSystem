using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconedNumberPresenter : MonoBehaviour
{
    [SerializeField] TEXT txt;
    [SerializeField] Image image;
    public void Init(string spriteName, Func<double> number) => Init(spriteName, number, NumberFormatter.defaultParams);
    public void Init(string spriteName, Func<double> number, NumberFormatter.Params _params)
    {
        InitAsync(spriteName, number, _params).Forget(); 
    }

    public async UniTask InitAsync(string spriteName, Func<double> number, NumberFormatter.Params _params) 
    {
        var sprite = await SpriteManager.GetSpriteAsync(spriteName);
        if (sprite != null) image.sprite = sprite;
        txt.BindNumberDelegate(number, _params);
    }

    public void Dispose()
    {
        image.sprite = null;
    }
}
