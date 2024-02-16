using Doozy.Editor.UIManager.Drawers;
using System;

namespace yayu.UI
{
    /// <summary>
    /// 今は不要
    /// </summary>
    public interface IUIUnit : IUIIdentify
    {
    }

    public abstract class UIUnit : IUIUnit
    {
        public UIUnit(string parentId, int index = -1)
        {
            id.SetParentId(parentId);
            id.SetIndex(index);
            factory = new UnitFactory(id.Path());
        }
        public abstract UIIdentify id { get; }
        protected UnitFactory factory { get; }
    }

    /// <summary>
    /// 不要かもしれない。Initを加えないと意味がない。これができるとだいぶ良い
    /// </summary>
    public interface IUIUnits
    {
    }

    public interface IUnitsAccessible
    {
        Type UnitType { get; }
        int Count { get; } 
    }

    public class UIUnits: UIElement, IUIUnits, IUnitsAccessible
    {
        public UIUnits(Func<int> unitsCount, string id) : base(id) 
        {
            //this.units = units;
            this.unitsCount = unitsCount;
        }
        //ICollection<object> units;
        Func<int> unitsCount;
        Type unitType;
   
        public Type UnitType => unitType;
        public int Count => unitsCount();
    }
}