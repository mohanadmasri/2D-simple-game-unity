using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb2d;

    public int runSpeed;

    private int jumpCount = 0;
    private bool canJump = true;

    public bool isGameOver = false;

    // الجديد: اللعبة لا تبدأ إلا بعد الضغط على Start
    private bool isGameStarted = false;

    Animator animition;

    public Text score;
    public float scoreInt = 0;

    public GameObject gameOverScreen;

    // الجديد: شاشة البداية أو زر البداية
    public GameObject startScreen;

    [Header("Game Over Settings")]
    public float gameOverDelay = 1.8f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animition = GetComponent<Animator>();

        // إيقاف اللعبة في البداية
        Time.timeScale = 0f;

        // تصفير السكور
        scoreInt = 0;

        if (score != null)
        {
            score.text = "Score: 0.000";
        }

        // إظهار شاشة البداية
        if (startScreen != null)
        {
            startScreen.SetActive(true);
        }

        // إخفاء شاشة الجيم أوفر
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    void Update()
    {
        // إذا اللعبة لم تبدأ أو انتهت، لا تحرك اللاعب ولا تزيد السكور
        if (!isGameStarted || isGameOver)
        {
            return;
        }

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

        if (score != null)
        {
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

    public void StartGame()
    {
        isGameStarted = true;

        Time.timeScale = 1f;

        if (startScreen != null)
        {
            startScreen.SetActive(false);
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
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}