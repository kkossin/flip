using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float jumpSpeed;
    public float fallSpeed;
    public float gravity;
    public bool grounded;
    public bool flipped;

    private CharacterController controller; 

    void Start () {
        controller = GetComponent<CharacterController>();
        grounded = true;
        flipped = false;
    }
	
	void Update () {
        isGrounded();
        jump();
        fall();
        flip();
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
            grounded = (Physics.Raycast(transform.position, -transform.up, controller.height*1.1f));
        }
        else
        {
            grounded = (Physics.Raycast(transform.position, transform.up, controller.height*1.1f));
        }
    }
}
