using System;

namespace yayu.Inventory
{
    public interface IItem
    {
        Guid id { get; }
        Func<string> name { get; }
        string iconPath { get; }
        Func<string> description { get; }
        string createdTime { get; }
    }

    public class MockItem : IItem
    {
        public Guid id { get; private set; }
        private string _name;
        private string _description;
        public string iconPath { get; private set; }
        public string createdTime { get; private set; }

        public Func<string> name
        {
            get { return () => _name; }
        }

        public Func<string> description
        {
            get { return () => _description; }
        }

        public MockItem(Guid id, string name, string iconPath, string description, string createdTime)
        {
            this.id = id;
            this._name = name;
            this.iconPath = iconPath;
            this._description = description;
            this.createdTime = createdTime;
        }
    }
}