using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yayu.UI
{
    public class RaycastDescriptionPresenterHandler : MonoBehaviour
    {
        [SerializeField] DescriptionPresenter presenter;
        IDescriptionTaker descTaker;
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debuger.Log("Raycast Hit");
                descTaker = hit.collider.GetComponent<IDescriptionTaker>();
                if (descTaker != null)
                {
                    YDebugger.Log(hit.collider.name);
                    presenter.SetDescription(descTaker);
                    return; // í«â¡ÇÃÉçÉWÉbÉNÇ≈ClearDescriptionÇåƒÇŒÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                }
            }
            else if (descTaker != null)
            {
                if (presenter.descTaker == descTaker) presenter.SetDescription(null);
                descTaker = null;
            }
        }
    }
}
