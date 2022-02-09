using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DigitSpanTest : MonoBehaviour
{
    public Text word;
    public Text problem;
    [SerializeField] private InputField answerInput;
    [SerializeField] private InputField[] wordInputs;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button submitWords;
    [SerializeField] private GameObject startWindow;
    [SerializeField] private GameObject endWindow;
    [SerializeField] private GameObject submitWindow;
    [SerializeField] private Button startButton;
    public List<string> selectedWordsList = new List<string>();
    public List<string> submitedWordsList = new List<string>();
    public string selectedProblem;
    public string selectedWord;
    public int answerIndex = 0;
    public float score = 0.0f;

    public int correctAnswer = 0;
    public int totalAnswer = 0;

    string[] questions = new string[] {"x + (3x - 2) = 18",
                                 "2(4 - x) - 3(x + 3) = -11",
                                 "2(x + 5) - 7 = 3(x - 2)",
                                 "2x = (24 / 3)",
                                 "x - 9 = -3"};

    int[] answers = new int[] {5,
                            2,
                            9,
                            4,
                            6
                            };

    public string[] words = {"replace", "hay", "trail", "carry", "decrease", "calendar", "anniversary",
                             "bicycle", "stool", "pressure"};

    public List<string> remainingWords = new List<string>();
    public List<string> remainingProblems = new List<string>();
    public List<int> remainingAnswers = new List<int>();

    void Awake()
    {
        int i = 0;
        for (i = 0; i < words.Length; i++)
        {
            remainingWords.Add(words[i]);
        }

        for (i = 0; i < questions.Length; i++)
        {
            remainingProblems.Add(questions[i]);
            remainingAnswers.Add(answers[i]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        endWindow.SetActive(false);
        submitWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void submitButtonOnClick()
    {
        int answer = int.Parse(answerInput.text);
        totalAnswer++;
        if (answer == remainingAnswers[answerIndex])
        {
            correctAnswer++;
            Debug.Log(correctAnswer);
            remainingProblems.Remove(selectedProblem);
            remainingAnswers.Remove(remainingAnswers[answerIndex]);
            remainingWords.Remove(selectedWord);
            if (correctAnswer == 5)
            {
                Debug.Log("TestCompleted");
                displayChosenWords();
                submitWindow.SetActive(true);
            }
        }
        if (correctAnswer < 5)
        {
            setProblem();
        }
    }

    public void submitWordOnClick()
    {
        int i;

        for (i = 0; i < 5; i++)
        {
            submitedWordsList.Add(wordInputs[i].text);
        }

        for (i = 5; i >= 0; i--)
        {
            if (submitedWordsList[i] == selectedWordsList[i])
            {
                score = score + 1;
                selectedWordsList.RemoveAt(i);
            }
        }

        submitedWordsList = submitedWordsList.Distinct().ToList();

        for(i = 0; i < 5; i++)
        {
                if (selectedWordsList.Contains(submitedWordsList[i]))
                {
                    score = score + 0.5f;
                }
        }
        submitWindow.SetActive(false);
        endWindow.SetActive(true);
    }


    public void setProblem()
    {
        answerIndex = Random.Range(0, remainingProblems.Count);
        selectedProblem = remainingProblems[answerIndex];
        selectedWord = remainingWords[Random.Range(0, remainingWords.Count)];

        problem.text = selectedProblem;
        word.text = selectedWord;
    }

    public void startTest()
    {
        answerIndex = Random.Range(0, remainingProblems.Count);
        selectedProblem = remainingProblems[answerIndex];
        selectedWord = remainingWords[Random.Range(0, remainingWords.Count)];

        problem.text = selectedProblem;
        word.text = selectedWord;
        selectedWordsList.Add(selectedWord);
        startWindow.SetActive(false);
    }

    void displayRemainingProblems()
    {
        int i = 0;
        for(i = 0; i < remainingProblems.Count; i++)
        {
            Debug.Log(remainingProblems[i]);
        }
    }

    void displayChosenWords()
    {
        int i = 0;
        for (i = 0; i < selectedWordsList.Count; i++)
        {
            Debug.Log(selectedWordsList[i]);
        }
    }
}
