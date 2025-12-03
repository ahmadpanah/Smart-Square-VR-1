using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class mainMenu : MonoBehaviour
{
    public TextMeshProUGUI TimeText;
    public int playerRotate = -300;
    void Start()
    {
        Time.timeScale = 1;
    }

    
    void Update()
    {
        var timeToDisplay = PlayerPrefs.GetFloat("highscore");

        var t0 = (int)timeToDisplay;
        var m = t0 / 60;
        var s = (t0 - m * 60);
        var ms = (int)((timeToDisplay - t0) * 100);

        TimeText.text = $"{m:00}:{s:00}:{ms:00}";

        gameObject.transform.Rotate(0, 0, playerRotate * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("block"))
        {
            collision.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color;
        }
    }

    public void playGame()
    {
        SceneManager.LoadScene(1);
    }
}
