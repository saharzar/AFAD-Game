using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Question
{
    public Sprite picture;
    public string description;
    public int correctAnswer; // 0 = 112, 1 = 110, 2 = 155
}

public class MiniGameManager : MonoBehaviour
{
    [Header("UI")]
    public Image questionImage;
    public TMP_Text questionText;
    public TMP_Text scoreText;
    public GameObject congratulationsScreen;

    [Header("Questions")]
    public Question[] questions;

    private int currentQuestion = 0;
    private int score = 0;

    void Start()
    {
        ShowQuestion(0);
        UpdateScore();
        congratulationsScreen.SetActive(false);
    }

    public void OnAnswer(int answerIndex)
    {
        Debug.Log("Button clicked: " + answerIndex);
        if (answerIndex == questions[currentQuestion].correctAnswer)
        {
            score++;
            UpdateScore();
        }

        currentQuestion++;

        if (currentQuestion < questions.Length)
        {
            ShowQuestion(currentQuestion);
        }
        else
        {
            ShowCongratulations();
        }
    }

    void ShowQuestion(int index)
    {
        questionImage.sprite = questions[index].picture;
        questionText.text = questions[index].description;
    }

    void UpdateScore()
    {
        scoreText.text = score + "/" + questions.Length;
    }

    void ShowCongratulations()
    {
        congratulationsScreen.SetActive(true);
    }
}
