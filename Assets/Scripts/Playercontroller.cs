using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public AudioClip strike;
    public AudioClip fireball;
    public GameManager gm;
    public float speed;
    public float jumpspeed = 1;
    private Rigidbody2D rb;
    private float moveInput;
    private bool facingRight = true;


    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private SpriteRenderer sr;
    private Animator anim;


    private int extraJump;
    public int extraJumpsValue;

    // Start is called before the first frame update
    void Start()
    {
        extraJump = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }


    // Update is called once per frame
    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);


        float xMOve = Input.GetAxis("Horizontal");

        transform.Translate(xMOve * speed * Time.deltaTime, 0, 0);

        if (xMOve != 0)
        {
            anim.SetBool("Moving", true);
            if (xMOve < 0)
            {
                sr.flipX = true;
            } else if (xMOve > 0)
            {
                sr.flipX = false;
            }
        }
        else
        {
            anim.SetBool("Moving", false);

        }


    }

    void Update()
    {
        if (isGrounded == true)
        {
            extraJump = extraJumpsValue;
        }


        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJump > 0)
        {
            rb.velocity = Vector2.up * jumpspeed;
            extraJump--;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJump == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpspeed;

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            gm.PlaySound(strike);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Score")
        {
            gm.IncrementScore(1);
            Destroy(collision.gameObject);
            gm.PlaySound(fireball);
        }

        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }

    }

}
