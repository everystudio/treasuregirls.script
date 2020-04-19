using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testforce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb2 = gameObject.GetComponent<Rigidbody2D>();
        rb2.AddForce(new Vector2(1.0f, 1.0f) * 250);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
