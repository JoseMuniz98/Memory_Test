using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StroopScript : MonoBehaviour
{
    public Color[] colorValues = { Color.green, Color.red, Color.blue, Color.yellow, Color.cyan, Color.magenta};
    public string[] colorNames = { "green", "red", "blue", "yellow", "cyan", "magenta" };
    [SerializeField] private Button[] buttons;
    [SerializeField] private Text[] buttonTexts;
    public Text targetColor;
    [SerializeField] private GameObject startWindow;
    [SerializeField] private GameObject endWindow;

    bool testActive = false;
    int selectedColor;

    public int hits = 0;
    public int clicks = 0;
    public int round = 0;
    public float startTime = 0.0f;
    public float endTime = 0.0f;
    public float totalTime = 0.0f;
    public float reactionAvg;

    // Start is called before the first frame update
    void Start()
    {
        disableAllButtons();
        endWindow.SetActive(false);
        Debug.Log(Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if((round == 10) && (testActive == true))
        {
            testActive = false;
            disableAllButtons();
            targetColor.text = " ";
            Debug.Log("Test Over");
            endTime = Time.time;
            totalTime = endTime - startTime;
            reactionAvg = totalTime / 10;
            Debug.Log(reactionAvg);
        }
    }

    public void chooseColor()
    {
        selectedColor = Random.Range(0, 6);
        targetColor.text = colorNames[selectedColor];
    }

    public void setColorValues()
    {
        int i, j, k;
        for (i = 0; i < 21; i++) {
            int clrVal = Random.Range(0, 6);
            int clrText = Random.Range(0, 5);

            if (clrText >= selectedColor)
            {
                clrText = clrText + 1;
            }

            if (clrText == clrVal)
            {
                k = 0;
                int[] clrOptions = { 0, 0, 0, 0, 0};
                for (j = 0; j < 6; j++)
                {
                    if(j != clrVal)
                    {
                        clrOptions[k] = j;
                        k++;
                    }
                }
                clrVal = clrOptions[Random.Range(0, 4)];
            }

            buttonTexts[i].color = colorValues[clrVal];
            buttonTexts[i].text = colorNames[clrText];
            if (clrText == selectedColor)
            {
                Debug.Log("Color Selected Innappropriately");
            }
        }
    }

    public void setTargetColor()
    {
        int setTarget = Random.Range(0, 20);
        buttonTexts[setTarget].text = colorNames[selectedColor];
        int targetVal = Random.Range(0, 5);
        if (targetVal >= selectedColor)
        {
            targetVal = targetVal + 1;
        }
        buttonTexts[setTarget].color = colorValues[targetVal];
    }

    public void startTest()
    {
        testActive = true;
        startTime = Time.time;
        shuffle();
        startWindow.SetActive(false);
        enableAllButtons();
    }

    public void disableAllButtons()
    {
        int i;
        for (i = 0; i < 21; i++)
        {
            buttons[i].enabled = false;
            buttonTexts[i].color = Color.black;
            buttonTexts[i].text = " ";
        }
    }

    public void enableAllButtons()
    {
        int i;
        for (i = 0; i < 21; i++)
        {
            buttons[i].enabled = true;
        }
    }

    public void shuffle()
    {
        chooseColor();
        setColorValues();
        setTargetColor();
    }
}
