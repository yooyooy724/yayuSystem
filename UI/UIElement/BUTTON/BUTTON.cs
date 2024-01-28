using System;
using UniRx;
using yayu.Event;

namespace yayu.UI
{
    /// <summary>
    /// 相対的にDomain側に公開
    /// </summary>
    public interface IButton
    {
        bool interactable { get; set; }
        bool visible { get; set; }
        void AddListener_Click(Action action);
        void AddListener_Enter(Action action);
        void AddListener_Exit(Action action);
        void RemoveListener_Click(Action action);
        void RemoveListener_Enter(Action action);
        void RemoveListener_Exit(Action action);
        void RemoveAllListeners();
    }

    /// <summary>
    /// 相対的にUI側に公開
    /// OnClickなどのTriggerとIsInteractableなどのStateを持つ
    /// </summary>
    public interface IButtonUIAccess
    {
        void OnClick();
        void OnEnter();
        void OnExit();
        bool IsInteractable();
        bool IsVisible();
    }

    /// <summary>
    /// PureなButtonクラス
    /// IButtonUIAccessを開発環境依存のIButtonに渡す
    /// </summary>
    public class UIButton : IButton, IButtonUIAccess
    {
        CustomEvent onClick = new(), onEnter = new(), onExit = new();

        public bool interactable { get; set; }
        public bool visible { get; set; }

        public void AddListener_Click(Action action) => onClick.AddListener(action);
        public void AddListener_Enter(Action action) => onEnter.AddListener(action);
        public void AddListener_Exit(Action action) => onExit.AddListener(action);

        public void RemoveListener_Click(Action action) => onClick.RemoveListener(action);
        public void RemoveListener_Enter(Action action) => onEnter.RemoveListener(action);
        public void RemoveListener_Exit(Action action) => onExit.RemoveListener(action);

        public virtual void RemoveAllListeners()
        {
            onClick.ClearListener();
            onEnter.ClearListener();
            onExit.ClearListener();
        }

        public virtual void OnClick() => onClick.Invoke();
        public void OnEnter() => onEnter.Invoke();
        public void OnExit() => onExit.Invoke();
        public bool IsInteractable() => interactable;
        public bool IsVisible() => visible;
    }

    /// <summary>
    /// Domain側がUI側にTriggerの発火を委譲し、Domain側の状態（Interactableなど）をUI側に流す
    /// </summary>
    public class UIElementConnection
    {
        public static void SetupButtonListenersAndStates(IButtonUIAccess button_domainSide, IButton button_UISIde)
        {
            button_UISIde.AddListener_Click(button_domainSide.OnClick);
            button_UISIde.AddListener_Enter(button_domainSide.OnEnter);
            button_UISIde.AddListener_Exit(button_domainSide.OnExit);
            button_domainSide.ObserveEveryValueChanged(_ => _.IsVisible()).Subscribe(_ => button_UISIde.visible = _);
            button_domainSide.ObserveEveryValueChanged(_ => _.IsInteractable()).Subscribe(_ => button_UISIde.interactable = _);
        }
        public static void SetupToggleListenersAndStates(IToggleUIAccess toggle_domainSide, IToggle toggle_UISIde)
        {
            toggle_UISIde.AddListener_Click(toggle_domainSide.OnClick);
            toggle_UISIde.AddListener_Enter(toggle_domainSide.OnEnter);
            toggle_UISIde.AddListener_Exit(toggle_domainSide.OnExit);
            toggle_domainSide.ObserveEveryValueChanged(_ => _.IsVisible()).Subscribe(_ => toggle_UISIde.visible = _);
            toggle_domainSide.ObserveEveryValueChanged(_ => _.IsInteractable()).Subscribe(_ => toggle_UISIde.interactable = _);
        }
    }
}
