using System;

namespace yayu.Inventory
{
    public interface IItem
    {
        Guid id { get; }
        string name { get; }
        string iconPath { get; }
        string description { get; }
        string createdTime { get; }
    }
}