using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private string playerWinObj;
    private string enemyObjectName;
    private GameBehavior gameBehavior;

    public GameObject gameManager;
    public AudioSource winSound;
    public AudioSource neutralSound;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameBehavior = gameManager.GetComponent<GameBehavior>();
        DeterminePlayerWinObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (PlayerWon(other.gameObject))
            {
                Debug.Log("Player Win");
                gameBehavior.Score++;
                winSound.Play();
                transform.position = Vector3.one * 9999f;
                Destroy(gameObject, neutralSound.clip.length);
            }
            else if (IsTie(other.gameObject))
            {
                Debug.Log("Player neutral");
                neutralSound.Play();
                transform.position = Vector3.one * 9999f;
                Destroy(gameObject, neutralSound.clip.length);
            }
            else
            {
                Debug.Log("PPlayer died");
                gameBehavior.CollisionEndGame();
            }
            //Debug.Log("Enemy hit!");
            ///Destroy(gameObject);
        }
    }

    private void DeterminePlayerWinObject()
    {
        switch (gameObject.name.ToLower())
        {
            case ("enemy rock(clone)"):
            case ("enemy rock"):
                playerWinObj = "paper";
                enemyObjectName = "rock";
                break;

            case ("enemy paper(clone)"):
            case ("enemy paper"):
                playerWinObj = "scissors";
                enemyObjectName = "paper";
                break;

            case ("enemy scissors(clone)"):
            case ("enemy scissors"):
                playerWinObj = "rock";
                enemyObjectName = "scissors";
                break;
        };
    }

    private bool IsTie(GameObject playerObject)
    {
        PlayerBehavior playerBehavior = playerObject.GetComponent<PlayerBehavior>();
        switch (enemyObjectName)
        {
            case "rock":
                return playerBehavior.rock.activeSelf;
            case "paper":
                return playerBehavior.paper.activeSelf;
            case "scissors":
                return playerBehavior.scissors.activeSelf;
            default:
                return false;
        }
    }

    private bool PlayerWon(GameObject playerObject)
    {
        PlayerBehavior playerBehavior = playerObject.GetComponent<PlayerBehavior>();
        if (playerBehavior.rock.activeSelf && playerWinObj == "rock") return true;
        if (playerBehavior.paper.activeSelf && playerWinObj == "paper") return true;
        if (playerBehavior.scissors.activeSelf && playerWinObj == "scissors") return true;
        return false;
    }

    public void DespawnEnemy()
    {
        Destroy(gameObject);
    }
}
