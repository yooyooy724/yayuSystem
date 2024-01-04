using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class UnityButton : BUTTON, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private CanvasGroup canvasGroup;
    private event Action onClick;
    private event Action onEnter;
    private event Action onExit;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component is not attached to the GameObject.");
        }
    }

    public override bool interactable
    {
        get => canvasGroup.interactable;
        set => canvasGroup.interactable = value;
    }

    public override bool visible
    {
        get => canvasGroup.alpha > 0f;
        set
        {
            canvasGroup.alpha = value ? 1f : 0f;
            canvasGroup.blocksRaycasts = value;
        }
    }

    public override void AddListener_onClick(Action action)
    {
        onClick += action;
    }

    public override void AddListener_onEnter(Action action)
    {
        onEnter += action;
    }

    public override void AddListener_onExit(Action action)
    {
        onExit += action;
    }

    public override void RemoveListener_onClick(Action action)
    {
        onClick -= action;
    }

    public override void RemoveListener_onEnter(Action action)
    {
        onEnter -= action;
    }

    public override void RemoveListener_onExit(Action action)
    {
        onExit -= action;
    }

    public override void RemoveAllListeners()
    {
        onClick = null;
        onEnter = null;
        onExit = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (interactable)
        {
            onClick?.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (interactable)
        {
            onEnter?.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (interactable)
        {
            onExit?.Invoke();
        }
    }
}
