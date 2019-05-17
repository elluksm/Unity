using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public Text highScoreText;
    public Text timeText;
    public Button startButton;
    private float highScore;
    private float timer;
    private bool isGameStarted;
    public Rigidbody rb;
    private float forwardForce = 200;
    private float speed = 100;

    // Start is called before the first frame update
    void Start()
    {
        highScore = 10000000000000.0f;
        timer = 0.0f;
        highScoreText.text = "";
        timeText.text = "";
        startButton.onClick.AddListener(OnStartButtonClick);
        rb = GetComponent<Rigidbody>();
    }

    void OnStartButtonClick()
    {
        startButton.gameObject.SetActive(false);
        timer = 0.0f;
        highScoreText.text = "";
        timeText.text = "";

        rb.transform.position = new Vector3(18, 0, -4);
        isGameStarted = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

         if (isGameStarted)
        {
            timer += Time.deltaTime;

            //add constant speed
            rb.AddForce(-forwardForce * Time.deltaTime, 0, 0);

            //speed and direction change with arrow keys
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(-moveHorizontal, 0.0f, -moveHorizontal);
            rb.AddForce(movement * speed);
        }


    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            EndGame();
        }

    }

    void EndGame()
    {
        isGameStarted = false;

        timeText.text = "Your time: "+ timer.ToString("0.00");
        if(timer < highScore)
        {
            highScore = timer;
        }
        highScoreText.text = "Best time: " + highScore.ToString("0.00");

        startButton.gameObject.SetActive(true);

    }

}
