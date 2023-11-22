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
            YDebugger.Log("OnPointerEnter");
            descTaker = eventData.pointerCurrentRaycast.gameObject.GetComponent<IDescriptionTaker>();
            if (descTaker != null)
            {
                YDebugger.Log(eventData.pointerCurrentRaycast.gameObject.name);
                presenter.SetDescription(descTaker);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // �z�o�[���O�ꂽ�Ƃ��Ƀe�L�X�g���N���A����
            if(presenter.descTaker == descTaker) presenter.SetDescription(null);
            descTaker = null;
        }
    }
}