/*
 *      This class exists for the sole purpose to
 *      inform the player which edit mode they're
 *      in. The efficiency isn't great since it's
 *      done through update, but what ever...
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModeUI : MonoBehaviour
{
    public LevelEditorController level;
    public TextMeshProUGUI text;


    void Update()
    {
        text.text = level.mode.ToString();
    }
}
