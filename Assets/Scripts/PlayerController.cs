using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public bool grounded;
    public bool flipped;
    public bool dead;

    private float jumpSpeed = 14;
    private float fallSpeed = 6;
    private float gravity = 20;

    //private CharacterController controller;
    private Rigidbody2D character;
    private SpriteRenderer sprite;
    private RaycastHit2D hit;  //shoots up
    private RaycastHit2D hit2; //shoots down

    void Start()
    {
        //controller = GetComponent<CharacterController>();
        character = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        grounded = false;
        flipped = false;
        dead = false;
    }

    void Update()
    {
        if (dead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            dead = false;
        }
        isGrounded();
        jump();
        fall();
        flip();
        checkForDeath();     
    }

    void jump()
    {
        if (Input.GetButtonDown("Jump") && grounded && !flipped)
        {
            fallSpeed = -jumpSpeed;
        }
        else if (Input.GetButtonDown("Jump") && grounded && flipped)
        {
            fallSpeed = jumpSpeed;
        }
    }

    void fall()
    {
        if (transform.position.y < -2.05f)
        {
            //fallSpeed = 0;
            //grounded = true;
            //transform.position = new Vector2(transform.position.x, -2.00f);
        }
        else if (transform.position.y > 2.02f)
        {
            //fallSpeed = 0;
            //grounded = true;
            //transform.position = new Vector2(transform.position.x, 2.05f);
        }

        if (!flipped)
        {
            if (!grounded)
            {
                fallSpeed += gravity * Time.deltaTime;
            }
            else
            {
                if (fallSpeed > 0) fallSpeed = 0;
            }
            character.MovePosition(new Vector2(-5.5f, transform.position.y - fallSpeed * Time.deltaTime));
        }
        else
        {
            if (!grounded)
            {
                fallSpeed += -gravity * Time.deltaTime;
            }
            else
            {
                if (fallSpeed < 0) fallSpeed = 0;
            }
            character.MovePosition(new Vector2(-5.5f, transform.position.y - fallSpeed * Time.deltaTime));
        }
    }

    void flip()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            flipped = !flipped;
            sprite.flipY = !sprite.flipY;
        }
    }

    void isGrounded()
    {
        if (!flipped)
        {
            grounded = (Physics2D.Raycast(transform.position, -Vector2.up, 0.5f));
        }
        else
        {
            grounded = (Physics2D.Raycast(transform.position, Vector2.up, 0.5f));
        }
    }

    void checkForDeath()
    {
        Vector2 top = new Vector2(transform.position.x, transform.position.y + transform.lossyScale.y); //check collision with top of character
        dead = Physics2D.Raycast(top, transform.right, transform.lossyScale.x);
        Vector2 bottom = new Vector2(transform.position.x, transform.position.y - transform.lossyScale.y); //check collision with bottom of character
        dead = Physics2D.Raycast(bottom, transform.right, transform.lossyScale.x);

        hit = Physics2D.Raycast(transform.position, transform.up, 0.5f);
        hit2 = Physics2D.Raycast(transform.position, -transform.up, 0.5f);
        if (hit && hit.collider.gameObject.CompareTag("Rocket"))
        {
            print("Hit2");
            dead = true;
        }
        else if (hit2 && hit2.collider.gameObject.CompareTag("Rocket"))
        {
            print("Hit2");
            dead = true;
        }
        else if (hit2 && hit2.collider.gameObject.CompareTag("Spikes"))
        {
            print("Hit2");
            dead = true;
        }
    }

    void onTriggerEnter2D(Collider2D other)
    {
        print("Hit1");
        if (other.gameObject.CompareTag("Rocket")) {
            dead = true;
        }
        else if (other.gameObject.CompareTag("Spikes"))
        {
            dead = true;
        }
    }
}
