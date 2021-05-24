using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParSlider : MonoBehaviour
{
    public Slider slider;
    public InputField textBox;

    //when we change the slider value, we need to change the input box to the new number
    public void SetBoxToSlider()
    {
        textBox.SetTextWithoutNotify(slider.value.ToString());
    }

    //when we make a new number in the box, we need to change the slider to the box's value
    public void SetSliderToBox(int newPar)
    {
        slider.value = newPar;
    }
}
