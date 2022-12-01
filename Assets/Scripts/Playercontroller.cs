using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
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


    private int extraJump;
    public int extraJumpsValue;

    // Start is called before the first frame update
    void Start()
    {
        extraJump = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();

    }


    // Update is called once per frame
    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);


        float xMOve = Input.GetAxis("Horizontal");

        transform.Translate(xMOve * speed * Time.deltaTime, 0, 0);

        if (facingRight == false && moveInput > 0){
            Flip();   
        } else if(facingRight == true && moveInput < 0)
        {
            Flip();
        }

        void Flip()
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;

        }
    }

    void Update()
    {
        if(isGrounded == true)
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
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Score")
        {
            Destroy(collision.gameObject); 
        }

    }

}
