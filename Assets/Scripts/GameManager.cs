using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameOver();
    }

    public void CheckGameOver()
    {
        if (gameOver)
        {
            Debug.Log("Game Over");
        }
    }
}
