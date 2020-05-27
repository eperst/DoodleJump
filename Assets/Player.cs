
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour { 

    private bool facingLeft;

    public float speed = 5.0f;
    private Rigidbody2D rb;
    public Sprite normalSprite;
    private float movement = 0.0f;
    public bool flippable = true;
    public bool falling = false;
    public bool hasJetpack = false;
    bool Higher { get; set; }
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        facingLeft = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal") * speed;
        if(hasJetpack == true && rb.velocity.y <= 2.0f)
        {
            hasJetpack = false;
            this.GetComponent<SpriteRenderer>().sprite = normalSprite;
        }
    }
    private void FixedUpdate()
    {
        if (transform.position.x < -3.0f)
        {
            float y = transform.position.y;
            transform.position = new Vector3(3.0f, y, -3.0f);
        }
        else if (transform.position.x > 3.0)
        {
            float y = transform.position.y;
            transform.position = new Vector3(-3.0f, y, -3.0f);
        }
        Vector2 v = rb.velocity;
        v.x = movement;
        rb.velocity = v;
        flip(movement);
    }

    void flip(float dir)
    {
        if(flippable && dir < 0 && facingLeft || dir > 0 && !facingLeft)
        {
            facingLeft = !facingLeft;
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }

}
