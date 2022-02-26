using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NBackTest : MonoBehaviour
{
    [SerializeField] private GameObject startWindow;
    [SerializeField] private GameObject endWindow;
    [SerializeField] private Button startButton;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button continueButton;
    [SerializeField] Text currentNumberText;
    [SerializeField] Text currentIndexText;
    [SerializeField] Text instructionsText;

    public float startTime = 0.0f;
    public float endTime = 0.0f;
    public float totalTime = 0.0f;
    public string totalTimeString = "0:00:00";

    private bool testStarted = false;
    private int currentNumberIndex = 0;
    private float hit = 0.0f;
    public float NBackAccuracy = 0.0f;
    private int[] numArray = new int[] { 0, 2, 1, 2, 0, 1, 0, 2, 1, 2,
                                         0, 1, 0, 2, 1, 2, 0, 1, 0, 2 };

    // Start is called before the first frame update
    void Start()
    {
        endWindow.SetActive(false);
        yesButton.enabled = false;
        yesButton.gameObject.SetActive(false);
        noButton.enabled = false;
        noButton.gameObject.SetActive(false);
        setArray();
        instructionsText.text = "Remember the numbers shown above.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setArray()
    {
        int i = 0;
        for (i = 0; i < 20; i++)
        {
            numArray[i] = Random.Range(0, 9);
        }
    }

    public void yesButtonOnClick()
    {
        if (numArray[currentNumberIndex] == numArray[currentNumberIndex - 3])
        {
            hit++;
        }
        setProblem();
    }

    public void noButtonOnClick()
    {
        if (numArray[currentNumberIndex] != numArray[currentNumberIndex - 3])
        {
            hit++;
        }
        setProblem();
    }

    public void nextButtonOnClick()
    {
        setProblem();
    }

    private void setProblem()
    {
        if (currentNumberIndex == 19)
        {
            endWindow.SetActive(true);
            NBackAccuracy = hit / 17;
            endTime = Time.time;
            return;
        }
        if(testStarted == false)
        {
            testStarted = true;
        }
        else
        {
            currentNumberIndex++;
        }

        if(currentNumberIndex == 3)
        {
            nextButton.enabled = false;
            nextButton.gameObject.SetActive(false);
            yesButton.gameObject.SetActive(true);
            yesButton.enabled = true;
            noButton.gameObject.SetActive(true);
            noButton.enabled = true;
            instructionsText.text = "Is this number the same as the one from 3 numbers ago.";
        }
        currentIndexText.text = (currentNumberIndex + 1).ToString();
        currentNumberText.text = numArray[currentNumberIndex].ToString();
    }

    public void startButtonOnClick()
    {
        startWindow.SetActive(false);
        setProblem();
        startTime = Time.time;
    }

    public void continueButtonOnClick()
    {
        Debug.Log(NBackAccuracy);
        totalTime = endTime - startTime;
        totalTimeString = formatTime(totalTime);
        PlayerPrefs.SetFloat("NBackScore", NBackAccuracy);
        PlayerPrefs.SetString("NBackTime", totalTimeString);
        SceneManager.LoadScene(2);
    }

    public string formatTime(float playtime)
    {
        float totalMinutes = playtime / 60;
        float hours = totalMinutes / 60;
        hours = Mathf.Round(hours * 1.0f) * 1f;
        float minutes = totalMinutes % 60;
        minutes = Mathf.Round(minutes * 1.0f) * 1f;
        float seconds = playtime % 60;
        seconds = Mathf.Round(seconds * 100.0f) * 0.01f;
        string playtimeString = hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString();
        return playtimeString;
    }
}
