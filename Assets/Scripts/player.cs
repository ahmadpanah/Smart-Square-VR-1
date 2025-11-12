using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class player : MonoBehaviour
{
    public float jumpVelocity = 1;
    private Rigidbody2D rb;
    public GameObject playerBody;
    public bool spinning;

    public bool doubleJumpActive;
    public int doubleJumpUsed;
    public float doubleJumpTimer;

    public GameObject boomCyan; // normal
    public GameObject boomGreen; // PowerUp -> Double Jump
    public GameObject boomGold; // PowerUp -> Inivisble

    public bool goldPower;
    public float goldPowerTimer;

    public bool gameOver;

    public TextMeshProUGUI TimeText;
    public float timer;
    public GameObject gameOverScreen;

    public Transform groundCheck;
    public LayerMask groundLayer;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerBody.GetComponent<SpriteRenderer>().color = Color.cyan;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == false)
        {
            timer += Time.deltaTime;
            DisplayTime(timer);

            spinning = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.2f, groundLayer);

            if (spinning)
            {
                doubleJumpUsed = 0;
            }
            if (doubleJumpActive)
            {
                if (Input.GetMouseButtonDown(0) && (spinning || doubleJumpUsed < 1))
                {
                    rb.velocity = Vector2.up * jumpVelocity;
                    if (!spinning)
                    {
                        doubleJumpUsed++;
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0) && spinning)
                {
                    rb.velocity = Vector2.up * jumpVelocity;
                }
            }
            if (spinning)
            {
                playerBody.transform.Rotate(0, 0, -350 * Time.deltaTime);
            }
            else
            {
                playerBody.transform.Rotate(0, 0, -700 * Time.deltaTime);
            }
        }
        else
        {
            gameOverScreen.SetActive(true);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("block") && gameOver == false)
        {
            collision.GetComponent<SpriteRenderer>().color = playerBody.GetComponent<SpriteRenderer>().color;
        }
        if (collision.gameObject.CompareTag("doubleJumpPowerUp"))
        {
            playerBody.GetComponent<SpriteRenderer>().color = Color.green;
            doubleJumpActive = true;
            goldPower = false;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("goldPowerUps"))
        {
            playerBody.GetComponent<SpriteRenderer>().color = Color.yellow;
            goldPower = true;
            doubleJumpActive = false;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Spike") && goldPower == false || collision.gameObject.CompareTag("outOfBounds"))
        {
            if (doubleJumpActive == true)
            {
                Instantiate(boomGreen, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            else if (goldPower == true)
            {
                Instantiate(boomGold, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            else
            {
                Instantiate(boomCyan, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            gameOver = true;
            playerBody.SetActive(false);
        }
    }
    private void DisplayTime(float timeToDisplay)
    {
        if (PlayerPrefs.GetFloat("highscore") < timeToDisplay)
        {
            PlayerPrefs.SetFloat("highscore", timeToDisplay);
        }
        var t0 = (int)timeToDisplay;
        var m = t0 / 60;
        var s = (t0 - m * 60);
        var ms = (int)((timeToDisplay - t0) * 100);

        TimeText.text = $"{m:00}:{s:00}:{ms:00}";
    }

}
