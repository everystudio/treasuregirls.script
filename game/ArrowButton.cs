using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowButton : MonoBehaviour
{
    public Button m_btn;
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
