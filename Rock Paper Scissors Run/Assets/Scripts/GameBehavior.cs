using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    public GameObject player;
    private PlayerBehavior playerBehavior;
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    public string labelText = "Run!";
    private int _score = 0;

    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            if (value % 3 == 0)
            {
                playerBehavior.IncreaseSpeed();
            }
        }
    }

    public void CollisionEndGame()
    {
        labelText = "Game Over";
        playerBehavior.StopCurrentTheme();
        GameEndCondition(labelText, false);
    }
    void GameEndCondition(string labelMsg, bool isWin)
    {
        labelText = labelMsg;
        if (isWin) showWinScreen = true;
        else showLossScreen = true;
        Time.timeScale = 0f;
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 50), "Score: " + _score);

        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 100), "You won!"))
            {
                RestartLevel();
            }
        }
        else if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 100), "Final Score: " + _score))
            {
                RestartLevel();
            }
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerBehavior = player.GetComponent<PlayerBehavior>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
