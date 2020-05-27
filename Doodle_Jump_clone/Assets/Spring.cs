
using UnityEngine;

public class Spring : MonoBehaviour
{
    private AudioSource sound;

    // collide
    public float jump = 10.0f;
    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0.0f)
        {
            sound.Play();
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                transform.localScale = new Vector3(transform.localScale.x / 1.5f, transform.localScale.y * 1.5f, transform.localScale.z);
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
