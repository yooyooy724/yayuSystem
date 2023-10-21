using UniRx;
using UnityEngine;

namespace yayu.ui
{
    public class DescriptionPresenter : MonoBehaviour
    {
        [SerializeField] TEXT header, description, footer;

        public IDescriptionTaker descTaker { get; private set; }

        public void SetDescription(IDescriptionTaker descTaker)
        {
            this.descTaker = descTaker;
        }
        void ClearTexts()
        {
            if (header != null) header.SetText(string.Empty);
            if (description != null) description.SetText(string.Empty);
            if (footer != null) footer.SetText(string.Empty);
        }

        [SerializeField] int intervalFrameCount = 10;
        void OnEnable()
        {
            Observable.IntervalFrame(intervalFrameCount).Subscribe(_ => UpdateTexts(descTaker)).AddTo(this);
        }

        void UpdateTexts(IDescriptionTaker descTaker)
        {
            if(descTaker == null)
            {
                ClearTexts();
                return;
            }

            if (header != null && descTaker.Header() != null)
                header.SetText(descTaker.Header().ToString());

            if (description != null && descTaker.Description() != null)
                description.SetText(descTaker.Description().ToString());

            if (footer != null && descTaker.Footer() != null)
                footer.SetText(descTaker.Footer().ToString());
        }


    }
}