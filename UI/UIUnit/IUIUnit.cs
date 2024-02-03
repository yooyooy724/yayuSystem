using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace yayu.UI
{
    /// <summary>
    /// 不要かもしれない。Initを加えないと意味がない。これができるとだいぶ良い
    /// </summary>
    public interface IUIUnit
    {
        void Register(string unitID, UIElementContainer container);
    }

    public class ButtonUnit : IUIUnit
    {
        public UIButton button = new UIButton("button");
        public UIText label = new UIText("label");

        public void Init(Action onClick, string label)
        {
            button.AddListener_Click(onClick);
            this.label.text = label;
        }

        public void Register(string unitID, UIElementContainer container)
        {
            button.parentId = unitID;
            label.parentId = unitID;
            container.Register(button, label);
        }
    }

    public class TestUI
    {
        readonly UIElementContainer container;
        public TestUI(UIElementContainer container)
        {
            this.container = container;
        }

        ButtonUnit[] buttonUnits;

        void Init(params (Action onClick, string label)[] infos)
        {
            buttonUnits = new ButtonUnit[infos.Length];
            for (int i = 0; i < infos.Length; i++)
            {
                buttonUnits[i] = new ButtonUnit();
                buttonUnits[i].Init(infos[i].onClick, infos[i].label);
                buttonUnits[i].Register($"test_ui_{i}", container);
            }
        }
    }
}