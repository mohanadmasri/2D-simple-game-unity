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

    [Header("Game Over Settings")]
    public float gameOverDelay = 1.8f;

    void Start()
    {
        gameOverScreen.SetActive(false);
        rb2d = GetComponent<Rigidbody2D>();
        animition = GetComponent<Animator>();
    }

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

            scoreInt = scoreInt + Time.deltaTime * 7;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;

        rb2d.linearVelocity = Vector2.zero;

        animition.SetTrigger("dead");

        Invoke("GameOverScreenShow", gameOverDelay);
    }

    private void GameOverScreenShow()
    {
        gameOverScreen.SetActive(true);
    }
}