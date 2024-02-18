using Doozy.Runtime.UIManager.Containers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using My.UI;

namespace My.StateMachine
{

    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public class UnityPanel : UIPanelMono
    {
        public enum InitialAction
        {
            Show,
            Hide,
            None
        }
        public override bool isOn
        {
            get => _isShow;
            set
            {
                if (isOn == value) return;
                if (value) Show();
                else Hide();
            }
        }
        bool _isShow;
        [SerializeField] InitialAction initialAction = InitialAction.None;
        [SerializeField] bool resetPosition = true;

        [Header("Switch Target")]
        [SerializeField] private bool switchCanvasGroup = true;
        [SerializeField] private bool switchGraphicRaycaster = true;
        [SerializeField] private bool switchGameObject = true;

        private RectTransform _rect;
        private RectTransform rect 
        {
            get
            {
                if (_rect == null) _rect = GetComponent<RectTransform>();
                return _rect;
            }
        }
        private CanvasGroup _canvasGroup;
        private CanvasGroup canvasGroup {    
            get
            {
                if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup>();
                return _canvasGroup;
            }
        }
        private GraphicRaycaster _graphicRaycaster;
        private GraphicRaycaster graphicRaycaster
        {
            get
            {
                if (_graphicRaycaster == null) _graphicRaycaster = GetComponent<GraphicRaycaster>();
                return _graphicRaycaster;
            }
        }
        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _graphicRaycaster = GetComponent<GraphicRaycaster>();

            switch (initialAction)
            {
                case InitialAction.Show:
                    Show();
                    break;
                case InitialAction.Hide:
                    Hide();
                    break;
                case InitialAction.None:
                    break;
            }
        }


        public override void Show()
        {
            // rectの座標を更新
            if(resetPosition) rect.anchoredPosition = new Vector2(0, 0);

            if (switchCanvasGroup)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }

            if (switchGraphicRaycaster)
            {
                graphicRaycaster.enabled = true;
            }

            if (switchGameObject)
            {
                gameObject.SetActive(true);
            }

            _isShow = true;
        }

        public override void Hide()
        {
            // rectの座標を更新
            if (resetPosition) rect.anchoredPosition = new Vector2(3000, 0);

            if (switchCanvasGroup)
            {
                canvasGroup.alpha = 0f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }

            if (switchGraphicRaycaster)
            {
                graphicRaycaster.enabled = false;
            }

            if (switchGameObject)
            {
                gameObject.SetActive(false);
            }

            _isShow = false;
        }
    }


}