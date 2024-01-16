using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ensures listed components are attached to the gameObject
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    public bool TestMode = false;
    public float speed = 7.0f;
    // Components
    Rigidbody2D rb;
    SpriteRenderer sr;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Component references grabbed through script
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if (speed <= 0)
        {
            speed = 7.0f;
            if (TestMode) Debug.Log("Speed too low! Object speed set to default value of 7.0f: " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);
    }
}
