using UnityEngine;
using UnityEngine.EventSystems;

namespace yayu.ui
{
    public enum DescriptionUIKind
    {
        UI_Description_Fixed
    }

    public class UIDescriptionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        static string TagName(DescriptionUIKind kind)
        {
            switch (kind)
            {
                case DescriptionUIKind.UI_Description_Fixed: return "UI_Description_Fixed";
                default: return "UI_Description_Fixed";
            }

        } 
        [SerializeField] DescriptionUIKind kind;
        DescriptionPresenter _presenter;
        DescriptionPresenter presenter 
        {
            get
            {
                if(_presenter == null)
                {
                    _presenter = GameObject.FindWithTag(TagName(kind)).GetComponent<DescriptionPresenter>();
                }
                return _presenter;
            }
        }

        IDescriptionTaker descTaker;

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debuger.Log("OnPointerEnter");
            descTaker = eventData.pointerCurrentRaycast.gameObject.GetComponent<IDescriptionTaker>();
            if (descTaker != null)
            {
                Debuger.Log(eventData.pointerCurrentRaycast.gameObject.name);
                presenter.SetDescription(descTaker);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // ホバーが外れたときにテキストをクリアする
            if(presenter.descTaker == descTaker) presenter.SetDescription(null);
            descTaker = null;
        }
    }
}