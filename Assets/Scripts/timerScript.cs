using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using System.Runtime.InteropServices;
//using static System.Net.Mime.MediaTypeNames;

public class timerScript : MonoBehaviour
{
    public Text timerText;
    static float timer;
    public Text lapText;
    public Text lapCounter;
    private int lap = 1;


    private float bestLapTime;
    public Text bestLapText;
    public string niceTime;

    public bool startLapTime = false;

    public bool checkpoint1 = false;
    public bool checkpoint2 = false;

    public int minutes;
    public int seconds;
    public int miliseconds;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        if (startLapTime == true)
        {
            timer += Time.deltaTime;
            //UnityEngine.Debug.Log(timer);

            minutes = Mathf.FloorToInt(timer / 60);
            seconds = Mathf.FloorToInt(timer - minutes * 60);
            miliseconds = Mathf.FloorToInt((timer * 1000) % 1000);

            int overallTime = minutes * seconds * miliseconds;

            string time = string.Format("{0:0}:{1:00}:{2:000}", minutes, seconds, miliseconds);

            timerText.text = time;

        }

        if (Input.GetKeyDown("9"))
        {
            RestartGame();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "startLine")
        {

            if (checkpoint1 == true && checkpoint2 == true)
            {

                lapText.text = "Last Lap: " + timerText.text;
                bestLapFunction();

                timer = 0.0f;
                checkpoint1 = false;
                checkpoint2 = false;

                lap += 1;
                lapCounter.text = "Lap: " + lap.ToString();
            }
            else
            {
                startLapTime = true;
                checkpoint1 = false;
                checkpoint2 = false;
            }
        }

        if (other.gameObject.name == "checkpoint1")
        {
            UnityEngine.Debug.Log("checkpoint1");
            checkpoint1 = true;
        }

        if (other.gameObject.name == "checkpoint2")
        {
            UnityEngine.Debug.Log("checkpoint2");
            checkpoint2 = true;
        }
    }

    public void bestLapFunction()
    {
        if (lap == 1)
        {
            bestLapTime = timer;
            bestLapText.text = "Best Lap: " + timerText.text;
        }
        else
        {
            if (timer < bestLapTime)
            {
                bestLapText.text = "Best Lap: " + timerText.text;
                bestLapTime = timer;
            }
        }
    }

}
