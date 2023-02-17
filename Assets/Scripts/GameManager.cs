using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public float score;
    private PlayerController playerControllerScript;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI gameOverText;

    private void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        gameOverText = GameObject.Find("GameOverText").GetComponent<TextMeshProUGUI>();
        gameOverText.text = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerControllerScript.gameOver)
        {
            if (playerControllerScript.isDashing)
            {
                score += 0.2f;
            }
            else
            {
                score += 0.1f;
            }
            scoreText.text = "Score: " + Mathf.Round(score);
        }
        else
        {
            // deactivate score text
            scoreText.gameObject.SetActive(false);
            gameOverText.text = "Game Over!\nFinal Score: " + Mathf.Round(score);
            // Debug.Log("Game Over, Final Score: " + score);
        }
    }
}
