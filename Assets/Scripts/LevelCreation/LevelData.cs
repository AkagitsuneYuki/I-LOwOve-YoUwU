using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LevelData
{
    public WallButton.WallType[] buttons;

    public LevelData(WallButton.WallType[] a)
    {
        buttons = a;
    }
}
