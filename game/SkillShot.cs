﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShot : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition += new Vector3(speed, 0.0f) * Time.deltaTime;


    }
}
