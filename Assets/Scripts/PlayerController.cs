using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private bool grounded;
    private bool flipped;
    private bool dead;
    public bool stopped;
    public int lives = 0;

    private float jumpSpeed = 7;
    private float fallSpeed = 6;
    private float gravity = 11;

    private Rigidbody2D character;
    private SpriteRenderer sprite;
    private Animator animator;
    private RaycastHit2D hitRight; //shoots right
    private RaycastHit2D hitUp;    //shoots up
    private RaycastHit2D hitDown;  //shoots down
    AudioSource switchTrack;
    AudioSource deathTrack;

    void Start()
    {
        character = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        grounded = false;
        flipped = false;
        dead = false;
        stopped = false;
		animator = GetComponent<Animator>();
        var audioSources = GetComponents<AudioSource>();
        switchTrack = audioSources[0];
        deathTrack = audioSources[1];
    }

    void Update()
    {
        jump();
        flip();
        checkforQuit();
    }

    void FixedUpdate()
    {
        if (dead)
        {
            //Update 11/18/16: No longer need the next line in order to make the death menu appear.
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//            
            stopGame();
        }
        isGrounded();
        fall();
        checkForCollision();
    }

    void jump()
    {
        if (Input.GetButtonDown("Fire2") && grounded && !flipped)
        {
			animator.SetTrigger ("jump");
			fallSpeed = -jumpSpeed;
        }
        else if (Input.GetButtonDown("Fire2") && grounded && flipped)
        {
			animator.SetTrigger ("jump");
			fallSpeed = jumpSpeed;
        }
    }

    void fall()
    {
        if (transform.position.y < -2.05f)
        {
            fallSpeed = 0;
            grounded = true;
            transform.position = new Vector2(transform.position.x, -2.05f);
        }
        else if (transform.position.y > 2.02f)
        {
            fallSpeed = 0;
            grounded = true;
            transform.position = new Vector2(transform.position.x, 2.02f);
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
            switchTrack.Play();
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
		if (grounded)
		{
			animator.SetTrigger("run");
		}
    }

    void checkForCollision()
    {
        Vector2 top = new Vector2(transform.position.x, transform.position.y); //check collision with top of character
        hitRight = Physics2D.Raycast(top, transform.right, transform.lossyScale.x);
        //Vector2 bottom = new Vector2(transform.position.x, transform.position.y - transform.lossyScale.y); //check collision with bottom of character
        //dead = Physics2D.Raycast(bottom, transform.right, transform.lossyScale.x);

        hitUp = Physics2D.Raycast(transform.position, transform.up, 0.5f);
        hitDown = Physics2D.Raycast(transform.position, -transform.up, 0.5f);

        //Rockets
        if (hitRight && hitRight.collider.gameObject.CompareTag("Rocket"))
        {
            //hitUp.transform.gameObject.GetComponent<AudioSource>().Play();
            if (lives == 0) { dead = true; }
            else lives--;
        }
        else if (hitUp && hitUp.collider.gameObject.CompareTag("Rocket"))
        {
            //hitUp.transform.gameObject.GetComponent<AudioSource>().Play();
            if (lives == 0) { dead = true; }
            else lives--;
        }
        else if (hitDown && hitDown.collider.gameObject.CompareTag("Rocket"))
        {
            //hitUp.transform.gameObject.GetComponent<AudioSource>().Play();
            if (lives == 0) { dead = true; }
            else lives--;
        }

        //Spikes
        else if (hitRight && hitRight.collider.gameObject.CompareTag("Spikes"))
        {
            if (lives == 0) { dead = true; }
            else lives--;
        }
        else if (hitUp && hitUp.collider.gameObject.CompareTag("Spikes"))
        {
            if (lives == 0) { dead = true; }
            else lives--;
        }
        else if (hitDown && hitDown.collider.gameObject.CompareTag("Spikes"))
        {
            if (lives == 0) { dead = true; }
            else lives--;
        }

        //Life Powerup
        else if (hitRight && hitRight.collider.gameObject.CompareTag("Life"))
        {
            lives++;
            Destroy(hitRight.transform.gameObject);
        }
        else if (hitUp && hitUp.collider.gameObject.CompareTag("Life"))
        {
            lives++;
            Destroy(hitUp.transform.gameObject);
        }
        else if (hitDown && hitDown.collider.gameObject.CompareTag("Life"))
        {
            lives++;
            Destroy(hitDown.transform.gameObject);
        }
        else if (hitRight)
        {
            dead = true;
        }
    }

    void onTriggerEnter2D(Collider2D other)
    {
        print("hitUp");
        if (other.gameObject.CompareTag("Rocket")) {
            if (lives == 0) { dead = true; }
            else lives--;
        }
        else if (other.gameObject.CompareTag("Spikes"))
        {
            if (lives == 0) { dead = true; }
            else lives--;
        }
        else if (other.gameObject.CompareTag("Life"))
        {
            lives++;
        }
    }
    
    void checkforQuit() {
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("Menu");
            GameObject.Find("Settings").GetComponent<FlipMenu>().startMusic();
        }
    }

    void stopGame()
    {
        if (!stopped) { deathTrack.Play(); }
        stopped = true;
        animator.SetTrigger("death");
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0) 
                 && animator.GetCurrentAnimatorStateInfo(0).IsName("death Animation"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        var gameOver = FindObjectOfType<Deathmenu>();
        gameOver.ShowButtons();
    }
}
