using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public bool grounded;
    public bool flipped;
    public bool dead;

    private float jumpSpeed = 4;
    private float fallSpeed = 0;
    private float gravity = 8;

    private CharacterController controller; 

    void Start () {
        controller = GetComponent<CharacterController>();
        grounded = false;
        flipped = false;
        dead = false;
    }
	
	void Update () {
        isGrounded();
        jump();
        fall();
        flip();
        checkForDeath();
        if (dead) {
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
            controller.Move(new Vector3(0, -fallSpeed) * Time.deltaTime);
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
            controller.Move(new Vector3(0, -fallSpeed) * Time.deltaTime);
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
            grounded = (Physics2D.Raycast(transform.position, -Vector2.up, controller.height/2.0f));
        }
        else
        {
            grounded = (Physics2D.Raycast(transform.position, Vector2.up, controller.height/2.0f));
        }
    }

    void checkForDeath ()
    {
        dead = Physics2D.Raycast(transform.position, Vector2.right, controller.height / 2.0f);
    }
}
