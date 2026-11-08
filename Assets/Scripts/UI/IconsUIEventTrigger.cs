using System;
using UnityEngine;

public static class IconsUIEventTrigger
{
    public static event Action<string> GetIconUI;

    public static void OnChangeIconUI(string icon)
    {
        GetIconUI?.Invoke(icon);
    }
}
