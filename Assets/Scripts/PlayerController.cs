using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    private int score = 0;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public Image winLoseBG;
    public Text winLoseText;
    public GameObject winLose;
    private bool isCoroutine = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            rb.AddForce(0, 0, speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            rb.AddForce(0, 0, -speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            rb.AddForce(-speed * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            rb.AddForce(speed * Time.deltaTime, 0, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            SetScoreText();
            //Debug.Log("Score: " + score);
        }
        else if (other.CompareTag("Trap"))
        {
            SetHealthText();
            //Debug.Log("Health: " + health);
        }
        else if (other.CompareTag("Goal"))
        {
            SetWin();
            StartCoroutine(LoadScene(3f));
            //Debug.Log("You win!");
        }
    }
     void Update()
    {
        if (health == 0 && isCoroutine)
        {
           // Debug.Log("Game Over!");
            SetGameOver();
            StartCoroutine(LoadScene(3f));
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    //Adds +1 to player's score window
    void SetScoreText()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    //Subtracts -1 from player's health window
    void SetHealthText()
    {
        health--;
        healthText.text = "Health: " + health.ToString();
    }

    //Displays You Win! when player reaches goal
    void SetWin()
    {
        winLose.SetActive(true);
        winLoseBG.color = Color.green;
        winLoseText.text = "You win!";
        winLoseText.color = Color.black;
    }

    //Displays Game Over! when player's health reaches 0
    void SetGameOver()
    {
        winLose.SetActive(true);
        winLoseBG.color = Color.red;
        winLoseText.text = "Game Over!";
        winLoseText.color = Color.white;
    }

    //Delays the game from restarting right away after win/loss
    IEnumerator LoadScene(float seconds)
    {
        isCoroutine = false;
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("maze");
        isCoroutine = true;
    }
}
