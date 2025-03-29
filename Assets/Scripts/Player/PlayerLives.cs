using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    public int maxLives = 3;
    public Image[] lifeIcons;

    public float restartDelay = 2f;

    private int currentLives;
    private bool isDead = false;

    void Start()
    {
        currentLives = maxLives;
        UpdateLifeUI();
    }

    public void LoseLife()
    {
        if (isDead) return;

        currentLives--;
        UpdateLifeUI();

        if (currentLives <= 0)
        {
            Die();
        }
    }

    void UpdateLifeUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = i < currentLives;
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("¡Jugador sin vidas! Reiniciando...");

        GetComponent<PlayerMovement>().enabled = false;

        Invoke("RestartGame", restartDelay);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            LoseLife();
        }
    }
}
