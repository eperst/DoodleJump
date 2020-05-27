
using UnityEngine;

public class Platform : MonoBehaviour
{
    // collide
    public float jump = 5f;
    private AudioSource bop;
    private void Start()
    {
        bop = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0.0f)
        {
            bop.Play();
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            Player p = collision.gameObject.GetComponent<Player>();
            if(rb != null && !p.falling)
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
        if (background.transform.position.y - 5.0f > transform.position.y)
        {
            Destroy(this.gameObject);
        }  
    }

}
