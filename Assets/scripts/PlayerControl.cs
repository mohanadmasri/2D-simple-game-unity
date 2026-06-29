using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb2d;
    public int runSpeed;
    private int jumpCount = 0;
    private bool canJump = true;
    public bool isGameOver = false;
    Animator animition;

    public Text score;
    public float scoreInt = 0;
    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        rb2d = GetComponent<Rigidbody2D>();
        animition = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            transform.position = Vector3.right * runSpeed * Time.deltaTime + transform.position;

            if (jumpCount == 2)
            {
                canJump = false;
            }

            if (Input.GetKeyDown("space") && canJump)
            {
                rb2d.linearVelocity = Vector3.up * 7.5f;
                animition.SetTrigger("jump");
                jumpCount += 1;
            }

            scoreInt = scoreInt + Time.deltaTime * 1087;
            score.text = "Score: " + scoreInt.ToString("0.000");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            canJump = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        animition.SetTrigger("dead");
        Invoke("GameOverScreenShow", 1f);
    }

    private void GameOverScreenShow()
    {
        gameOverScreen.SetActive(true);
    }
}