using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    // collide
    public float jump = 5f;
    private bool jumped = false;
    private AudioSource bop;
    private bool soundPlayed = false;
    private void Start()
    {
        bop = GameObject.Find("Doodler").GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0.0f)
        {
            bop.Play();
            soundPlayed = true;
            jumped = true;
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 v = rb.velocity;
                v.y = jump;
                rb.velocity = v;
            }
        }
    }

    private void FixedUpdate()
    {
        GameObject background = GameObject.Find("BackgroundWide");
        if (background.transform.position.y - 5.0f > transform.position.y || jumped )
        {
            gameObject.SetActive(false);
           // Destroy(this.gameObject);
        }
    }
}
