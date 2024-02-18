using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.UI
{
    public class UIElementMono : MonoBehaviour, IUIElement
    {
        public string parentId { set => id.SetParentId(value); }
        public void SetActive(bool isActive) => gameObject.SetActive(isActive);
        public virtual Type UIAccessible => null;
        UIIdentify _id;
        public UIIdentify id 
        {
            get
            {
                if(_id == null) _id = new UIIdentify(GetId(gameObject.name, this));
                return _id;
            }
        }

        public static string GetId(string name, MonoBehaviour element)
        {
            var typeId = GetTypeId(element);
            return GetId(name, typeId);
        }

        public static string GetId(string name, string typeId)
        {
            // "|"で分割し、全空白を除く
            var parts = name.Replace(" ", string.Empty).Split('|');

            // 型Idで始まるものを見つける
            foreach (var part in parts)
            {
                if (part.StartsWith(typeId))
                {
                    // 型Idを消しidとして返す
                    return part.Substring(typeId.Length);
                }
            }

            // 該当するものがない場合はnameをそのまま返す
            return default;
        }

        static string GetTypeId(object element)
        {
            // elementの型に基づいて型IDを返す
            switch (element)
            {
                case IButton _:
                    return "(button)";
                case IPanel _:
                    return "(panel)";
                case IText _:
                    return "(text)";
                case IToggle _:
                    return "(toggle)";
                case IToggleStateApplier _:
                    return "(toggle)";
                case IGauge _:
                    return "(gauge)";
                case IUIUnits _:
                    return "(units)";
                default:
                    return string.Empty; // 既知の型ではない場合は空文字を返す
            }
        }

    }
}