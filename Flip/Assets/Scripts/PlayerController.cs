using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public bool grounded;
    public bool flipped;
    public bool dead;

    private float jumpSpeed = 4;
    private float fallSpeed = 0;
    private float gravity = 8;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        grounded = false;
        flipped = false;
        dead = false;
    }

    void Update()
    {
        isGrounded();
        jump();
        fall();
        flip();
        checkForDeath();
        if (dead)
        {
            SceneManager.LoadScene("Prototype");
            dead = false;
        }
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
        if (transform.position.y < -2.00f)
        {
            fallSpeed = 0;
            grounded = true;
            transform.position = new Vector2(transform.position.x, -2.00f);
        }
        else if (transform.position.y > 2.05f)
        {
            fallSpeed = 0;
            grounded = true;
            transform.position = new Vector2(transform.position.x, 2.05f);
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
            controller.Move(new Vector2(0, -fallSpeed) * Time.deltaTime);
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
            controller.Move(new Vector2(0, -fallSpeed) * Time.deltaTime);
        }
    }

    void flip()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            flipped = !flipped;
        }
    }

    void isGrounded()
    {
        if (!flipped)
        {
            grounded = (Physics2D.Raycast(transform.position, -Vector2.up, controller.height / 2.0f));
        }
        else
        {
            grounded = (Physics2D.Raycast(transform.position, Vector2.up, controller.height / 2.0f));
        }
    }

    void checkForDeath()
    {
        dead = Physics2D.Raycast(transform.position, Vector2.right, controller.height / 2.0f);
    }
}
