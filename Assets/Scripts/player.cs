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
            } else
            {
                playerBody.transform.Rotate(0, 0, -700 * Time.deltaTime);
            }
        } else
        {
            gameOverScreen.SetActive(true);
        }
    }
}
