using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

namespace yayu.StateMachine
{

    [RequireComponent(typeof(RectTransform))]
    public class ViewState : MONO_STATE
    {
        [SerializeField] private string _id;

        [SerializeField]
        private bool
            switchCanvasGroup = true,
            switchGraphicRaycaster = false,
            switchGameObject = true;

        private RectTransform rect;
        private CanvasGroup canvasGroup;
        private GraphicRaycaster graphicRaycaster;
        public override string id { get => _id; }

        private void Awake()
        {
            rect = GetComponent<RectTransform>();

            if (switchCanvasGroup)
            {
                canvasGroup = GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
            }

            if (switchGraphicRaycaster)
            {
                graphicRaycaster = GetComponent<GraphicRaycaster>();
                if (graphicRaycaster == null)
                {
                    graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
                }
            }
        }

        public override void Enter()
        {
            // rectの座標を更新
            rect.anchoredPosition = new Vector2(0, 0);

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
        }

        public override void Exit()
        {
            // rectの座標を更新
            rect.anchoredPosition = new Vector2(3000, 0);

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
        }
    }


}