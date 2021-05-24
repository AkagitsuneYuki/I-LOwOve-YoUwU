using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParText : MonoBehaviour
{
    public ParSlider parSlider;
    public InputField input;

    //check to see if what the user typed is a number and then set the par to that number
    public void CheckTextForInt()
    {
        try
        {
            //get the number from the text
            int x = int.Parse(input.text);
            //if the number exceeds the max, then change it to the max
            if(x > 99)
            {
                x = 99;
            }
            //if the number is below 0, then set it to 0
            else if(x < 0)
            {
                x = 0;
            }
            //set the slider to the box
            parSlider.SetSliderToBox(x);
        }
        //this shouldn't happen since the input field is set to only take ints in the editor
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }
}
