using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool grounded;
    private bool flipped;
    private bool dead;
    private bool shielded;
    private bool slowed;
    private bool recorded;
    public bool stopped;
    public int lives = 0;
    private int timer = 0;

    private float jumpSpeed = 7;
    private float fallSpeed = 6;
    private float gravity = 11;
    private float gameSpeed;
    Color originalColor;

    private Rigidbody2D character;
    private SpriteRenderer sprite;
    private Animator animator;
    private Text timerDisplay;
    private RaycastHit2D hitRight; //shoots right
    private RaycastHit2D hitUp;    //shoots up
    private RaycastHit2D hitDown;  //shoots down
    AudioSource switchTrack;
    AudioSource deathTrack;

    void Start()
    {
        character = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        timerDisplay = GameObject.Find("Timer").GetComponent<Text>();
        animator = GetComponent<Animator>();
        var audioSources = GetComponents<AudioSource>();
        switchTrack = audioSources[0];
        deathTrack = audioSources[1];
        originalColor = sprite.color;
        grounded = false;
        flipped = false;
        dead = false;
        shielded = false;
        slowed = false;
        recorded = false;
        stopped = false;	
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
            if (!recorded)
            {
                int score = GameObject.Find("Level Manager").GetComponent<LevelController>().score;
                if (GameObject.Find("Settings") != null)
                {
                    GameObject.Find("Settings").GetComponent<FlipMenu>().addScore(score);
                }
                recorded = true;
            }
        }
        isGrounded();
        fall();
        checkForCollision();
        if (shielded) applyShield();
        if (slowed) slowGame();
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
        if (!flipped && transform.position.y <= -2.05f)
        {           
            grounded = true;
            animator.SetTrigger("run");
        }
        else if (flipped && transform.position.y >= 2.05f)
        {            
            grounded = true;
            animator.SetTrigger("run");           
        }
        else if (!flipped)
        {
            if (transform.position.y > 2.05f)
            {
                fallSpeed = 0;
                transform.position = new Vector2(transform.position.x, 2.05f);
            }
            grounded = Physics2D.Raycast(transform.position, -Vector2.up, 0.5f);
        }
        else
        {
            if (transform.position.y < -2.05f)
            {
                fallSpeed = 0;
                transform.position = new Vector2(transform.position.x, -2.05f);
            }
            grounded = Physics2D.Raycast(transform.position, -Vector2.down, 0.6f);
        }

		if (grounded)
		{
			animator.SetTrigger("run");
		}
    }

    void checkForCollision()
    {
        hitRight = Physics2D.Raycast(transform.position, transform.right, transform.lossyScale.x);
        hitUp = Physics2D.Raycast(transform.position, transform.up, 0.5f);
        hitDown = Physics2D.Raycast(transform.position, -transform.up, 0.5f);

        //Rockets
        if (hitRight && hitRight.collider.gameObject.CompareTag("Rocket"))
        {
            if (lives == 0 && !shielded) { dead = true; }
            else
            {
                Destroy(hitRight.transform.gameObject);
                if (!shielded) lives--;
            }
        }
        else if (hitUp && hitUp.collider.gameObject.CompareTag("Rocket"))
        {
            if (lives == 0 && !shielded) { dead = true; }
            else
            {
                Destroy(hitUp.transform.gameObject);
                if (!shielded) lives--;
            }
        }
        else if (hitDown && hitDown.collider.gameObject.CompareTag("Rocket"))
        {
            if (lives == 0 && !shielded) { dead = true; }
            else
            {
                Destroy(hitDown.transform.gameObject);
                if (!shielded) lives--;
            }
        }

        //Spikes
        else if (hitRight && hitRight.collider.gameObject.CompareTag("Spikes"))
        {
            if (lives == 0 && !shielded) { dead = true; }
            else
            {
                Destroy(hitRight.transform.gameObject);
                if (!shielded) lives--;
            }
        }
        else if (hitUp && hitUp.collider.gameObject.CompareTag("Spikes"))
        {
            if (lives == 0 && !shielded) { dead = true; }
            else
            {
                Destroy(hitUp.transform.gameObject);
                if (!shielded) lives--;
            }
        }
        else if (hitDown && hitDown.collider.gameObject.CompareTag("Spikes"))
        {
            if (lives == 0 && !shielded) { dead = true; }
            else
            {
                Destroy(hitDown.transform.gameObject);
                if (!shielded) lives--;
            }
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

        //Shield Powerup
        else if (hitRight && hitRight.collider.gameObject.CompareTag("Shield"))
        {
            shielded = true;
            timer = 300;
            Destroy(hitRight.transform.gameObject);
        }
        else if (hitUp && hitUp.collider.gameObject.CompareTag("Shield"))
        {
            shielded = true;
            timer = 300;
            Destroy(hitUp.transform.gameObject);
        }
        else if (hitDown && hitDown.collider.gameObject.CompareTag("Shield"))
        {
            shielded = true;
            timer = 300;
            Destroy(hitDown.transform.gameObject);
        }

        //slowTime Powerup
        else if (hitRight && hitRight.collider.gameObject.CompareTag("Slow Time"))
        {
            if (!slowed)
            {
                slowed = true;
                timer = 300;
            }
            Destroy(hitRight.transform.gameObject);
        }
        else if (hitUp && hitUp.collider.gameObject.CompareTag("Slow Time"))
        {
            if (!slowed)
            {
                slowed = true;
                timer = 300;
            }
            Destroy(hitUp.transform.gameObject);
        }
        else if (hitDown && hitDown.collider.gameObject.CompareTag("Slow Time"))
        {
            if (!slowed)
            {
                slowed = true;
                timer = 300;
            }
            Destroy(hitDown.transform.gameObject);
        }

        else if (hitRight && !dead)
        {
            if (lives == 0 && !shielded) { dead = true; }
            else
            {
                Destroy(hitRight.transform.gameObject);
                if (!shielded) lives--;
            }
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
    
    void applyShield()
    {
        if (timer % 15 == 0)
        {
            if (timer % 30 == 0)
            {
                sprite.color = Color.green;
                if (timer % 60 == 0)
                {
                    int timerSeconds = timer / 60;
                    if (timerSeconds == 0) { timerDisplay.text = ""; }
                    else { timerDisplay.text = timerSeconds.ToString(); }
                }
            }
            else
            {
                sprite.color = originalColor;
            }
        }
        if (timer > 0)
        {
            timer--;
        }
        else
        {
            shielded = false;
            sprite.color = originalColor;
            timer = 0;
        }
    }

    void slowGame()
    {
        if (timer == 300)
        {
            gameSpeed = GameObject.Find("Level Manager").GetComponent<LevelController>().speed;
            GameObject.Find("Level Manager").GetComponent<LevelController>().speed = gameSpeed / 2;
        }
        if (timer % 60 == 0)
        {
            int timerSeconds = timer / 60;
            if (timerSeconds == 0) { timerDisplay.text = ""; }
            else { timerDisplay.text = timerSeconds.ToString(); }
        }
        if (timer > 0)
        {
            timer--;
        }
        else
        {
            slowed = false;
            GameObject.Find("Level Manager").GetComponent<LevelController>().speed = gameSpeed;
            timer = 0;
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
