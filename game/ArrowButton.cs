using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowButton : MonoBehaviour
{
    public bool is_press;

    public void OnPointerDown()
    {
        is_press = true;
    }
    public void OnPointerUp()
    {
        is_press = false;
    }


}
