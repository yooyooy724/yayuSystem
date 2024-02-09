using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace yayu.UI
{
    /// <summary>
    /// 不要かもしれない。Initを加えないと意味がない。これができるとだいぶ良い
    /// </summary>
    public interface IUnits
    {
        //void SetUnit(List<T> units);
    }

    public interface IUnitsAccessible
    {
        Type UnitType { get; }
        int Count { get; } 
    }

    public class UIUnits: UIElement, IUnits, IUnitsAccessible
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