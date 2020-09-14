using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBehavior : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 direction;
    public AudioSource[] countDownAudioArray = new AudioSource[4];
    public AudioSource[] themeArray;
    public AudioSource scissorsTheme;
    public AudioSource rockTheme;
    public AudioSource paperTheme;
    public float speed;
    private float hInput = 0.0f;
    public float jumpForce;
    public float gravity = -15;
    public GameObject rock;
    public GameObject scissors;
    public GameObject paper;
    private GameObject currentModel;
    private AudioSource currentTheme;
    private List<GameObject> playerModels;
    private int playerModelIndex = 0;
    private float timer;
    private int countDown = 3;

    private GameBehavior gameBehavior;

    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Player";
        rock.SetActive(false);
        paper.SetActive(false);
        scissors.SetActive(false);
        InitializePlayerModels();
        InitializePlayerThemes();
        currentModel = scissors;
        currentTheme = scissorsTheme;
        timer = Random.Range(3.2f, 30f);
        SwitchToNextPlayerModel();
        
        direction.z = speed;
        characterController = GetComponent<CharacterController>();

        gameManager = GameObject.Find("GameManager");
        gameBehavior = gameManager.GetComponent<GameBehavior>();
        //StartCoroutine(SwitchToNextPlayerModel());
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0.0f)
        {
            if (timer < countDown)
            {
                countDownAudioArray[countDown].Play();
                countDown--;
            }
        }
        else
        {
            countDownAudioArray[0].Play();
            timer = Random.Range(3.2f, 25f);
            countDown = 3;
            SwitchToNextPlayerModel();
        }
        if (!characterController.isGrounded)
            direction.y += gravity * Time.deltaTime;
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        hInput = Input.GetAxis("Horizontal") * speed;

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    SwitchToNextPlayerModel();
        //}
    }


    private void FixedUpdate()
    {
        characterController.Move(direction * Time.fixedDeltaTime + this.transform.right * hInput * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    //private bool IsGrounded()
    //{
    //    Physics.Raycast(gameObject.transform.position, Vector3.down,)
    //}

    private void InitializePlayerThemes()
    {
        themeArray = new AudioSource[3] { scissorsTheme, rockTheme, paperTheme };
    }

    private void InitializePlayerModels()
    {
        playerModels = new List<GameObject>();
        playerModels.Add(scissors);
        playerModels.Add(rock);
        playerModels.Add(paper);
    }
    private void SwitchToNextPlayerModel()
    {
        if (playerModels.Count == 0) return;
        currentModel.SetActive(false);
        currentTheme.Stop();
        var nextModel = playerModels[playerModelIndex];
        var nextTheme = themeArray[playerModelIndex];
        nextTheme.Play();
        nextModel.SetActive(true);
        currentModel = nextModel;
        currentTheme = nextTheme;
        playerModelIndex = (playerModelIndex + 1) % playerModels.Count;
        //while (true)
        //{
        //    //int time = Random.Range(3, 20);
        //    currentModel.SetActive(false);
        //    var nextModel = playerModels[playerModelIndex];
        //    nextModel.SetActive(true);
        //    currentModel = nextModel;
        //    playerModelIndex = (playerModelIndex + 1) % playerModels.Count;
        //    //yield return new WaitForSecondsRealtime(time);
        //}
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.point.z > transform.position.z + hit.controller.radius)
        {
            Death();
        }
    }
    private void Death()
    {
        Debug.Log("Game over!");
        StopCurrentTheme();
        gameBehavior.CollisionEndGame();
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Collison detected " + collision.gameObject.name);
    //    if (collision.contactCount > 0)
    //    {
    //        ContactPoint contactPoint = collision.contacts[0];
    //        if (!CollisionFromBottom(contactPoint))
    //        {
    //            Debug.Log("Game over!");
    //        }
    //    }
    //}

    private bool CollisionFromBottom(ContactPoint contactPoint)
    {
        return Vector3.Dot(contactPoint.normal, Vector3.up) > 0.5;
    }

    public void IncreaseSpeed()
    {
        direction.z += 1.2f;
    }

    public void StopCurrentTheme()
    {
        currentTheme.Stop();
    }
}
