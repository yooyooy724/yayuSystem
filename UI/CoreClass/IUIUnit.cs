using System;

namespace yayu.UI
{
    public interface IUIUnit : IUIIdentify
    {
    }

    public abstract class UIUnit : IUIUnit
    {
        public abstract UIIdentify id { get; }
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