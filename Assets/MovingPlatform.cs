using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // collide
    public float jump = 5f;
    public float step = 0.0001f;
    bool direction = true;
    private AudioSource bop;
    private void Start()
    {
        bop = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0.0f)
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                bop.Play();
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
    void Update()
    {
        if(direction)
        {
            if (transform.position.x < 2.3f)
            {
                transform.Translate(Vector3.right * Time.deltaTime);
            } else
            {
                direction = false;
            }
            
        } else if(!direction)
        {
            if (transform.position.x > -2.3f)
            {
                transform.Translate(Vector3.left * Time.deltaTime);
            }
            else
            {
                direction = true;
            }

        }
        
    }
}
