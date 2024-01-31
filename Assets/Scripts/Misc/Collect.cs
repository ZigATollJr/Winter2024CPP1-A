using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public AudioSource sound;
    private bool soundPlayed = false;
    private float desiredAlpha = 1.0f;
    private float currentAlpha = 1.0f;
    // Start is called before the first frame update

    // NEED TO ADD TEST BOOL!!!

    void Start()
    {
        sound = GetComponent<AudioSource>();

        Debug.Log("started");
    }

    void Update()
    {
        // Put into a co-routine once we learn how to do that. For now, will run eternally!
        
        // Code from Kurt-Dekker on the unity forms.
        currentAlpha = Mathf.MoveTowards(currentAlpha, desiredAlpha, 1.0f * Time.deltaTime);
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, currentAlpha);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") == true)
        {
            if (soundPlayed == false)
            {
                sound.Play();
                soundPlayed = true;
                Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
                rb.mass = 5;
                rb.velocity = new Vector2(0, 5);
                desiredAlpha = 0.0f;
            }
            Destroy(gameObject, 1.0f);
        }
    }
}
