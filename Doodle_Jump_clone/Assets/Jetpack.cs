
using System.Numerics;
using UnityEngine;

public class Jetpack : MonoBehaviour
{

    public float jump = 10.0f;
    public Sprite jetPlayer;
    private GameObject player;
    private AudioSource sound;
    GameObject background;
    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        player = GameObject.Find("Doodler");
        background = GameObject.Find("BackgroundWide");

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            background.GetComponent<AudioSource>().Play(); 
            Destroy(GetComponent<BoxCollider2D>());
            this.transform.localScale = new UnityEngine.Vector3(0.0f, 0.0f, 0.0f);
            transform.position = new UnityEngine.Vector3(transform.position.x, 0.0f, 10.0f);
            
            if(!player.GetComponent<Player>().hasJetpack)
            {
                sound.Play();
                player.GetComponent<SpriteRenderer>().sprite = jetPlayer;
                UnityEngine.Vector2 v = rb.velocity;
                v.y = jump;
                rb.velocity = v;
                player.GetComponent<Player>().hasJetpack = true;
            }
        }   
    }
    private void FixedUpdate()
    {
        if (player.GetComponent<Player>().hasJetpack)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        } else
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
        }

        if (background.transform.position.y - 5.0f > transform.position.y)
        {
            Destroy(this.gameObject);
        }
    }
}
