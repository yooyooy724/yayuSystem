using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class CustomLongPress : MonoBehaviour
{
    [SerializeField] private Graphic raycastTarget; // このRaycastTargetに対する参照をInspectorからアサインします
    [SerializeField] private float longPressTime = 2f; // 長押しとみなす時間

    private ObservablePointerEnterTrigger enterTrigger;
    private ObservablePointerDownTrigger downTrigger;
    private ObservablePointerExitTrigger exitTrigger;
    private ObservablePointerUpTrigger upTrigger;

    private void OnEnable()
    {
        // マウストリガーの初期化
        enterTrigger = raycastTarget.gameObject.AddComponent<ObservablePointerEnterTrigger>();
        downTrigger = raycastTarget.gameObject.AddComponent<ObservablePointerDownTrigger>();
        exitTrigger = raycastTarget.gameObject.AddComponent<ObservablePointerExitTrigger>();
        upTrigger = raycastTarget.gameObject.AddComponent<ObservablePointerUpTrigger>();

        // マウスがraycastTargetの上にある間、長押しを判定

        // マウスがraycastTargetの上に入ったときを観測
        enterTrigger.OnPointerEnterAsObservable()
            // マウスがraycastTargetの上に入った後、マウスが押されたときを観測
            .SelectMany(_ => downTrigger.OnPointerDownAsObservable())
            // マウスが押された状態を毎フレーム観測
            .SelectMany(_ => Observable.EveryUpdate())
            // マウスがraycastTargetから出たとき、またはマウスが離されたときに観測を停止
            .TakeUntil(exitTrigger.OnPointerExitAsObservable())
            .TakeUntil(upTrigger.OnPointerUpAsObservable())
            // 経過時間を算出
            .Select(_ => Time.deltaTime)
            // 押されてからの合計時間を計算
            .Scan((pastTime, deltaTime) => pastTime + deltaTime)
            // 押されてからの合計時間が長押しの時間を超えた場合にのみ通知
            .Where(pastTime => pastTime >= longPressTime)
            // 長押しが検出されたときにログ出力
            .Subscribe(_ => Debug.Log("Long press detected"))
            // このGameObjectが破棄されたときに自動的に購読解除するよう設定
            .AddTo(this);

    }
}
