using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Question
{
    public Sprite picture;
    public string description;
    public int correctAnswer;
}

public class MiniGameManager : MonoBehaviour
{
    [Header("UI")]
    public Image questionImage;
    public TMP_Text questionText;
    public TMP_Text scoreText;

    [Header("Congratulations")]
    public CanvasGroup congratulationsScreen;

    [Header("Questions")]
    public Question[] questions;

    private int currentQuestion = 0;
    private int score = 0;

    void Start()
    {
        ShowQuestion(0);
        UpdateScore();

        // Start invisible
        congratulationsScreen.alpha = 0;
        congratulationsScreen.interactable = false;
        congratulationsScreen.blocksRaycasts = false;
    }

    public void OnAnswer(int answerIndex)
    {
        if (currentQuestion >= questions.Length)
            return;

        if (answerIndex == questions[currentQuestion].correctAnswer)
        {
            score++;
            UpdateScore();

            currentQuestion++;

            if (currentQuestion < questions.Length)
            {
                ShowQuestion(currentQuestion);
            }
            else
            {
                StartCoroutine(FadeCongratulations());
            }
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

    IEnumerator FadeCongratulations()
    {
        congratulationsScreen.interactable = true;
        congratulationsScreen.blocksRaycasts = true;

        float duration = 1f;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;

            congratulationsScreen.alpha = Mathf.Lerp(0, 1, time / duration);

            yield return null;
        }

        congratulationsScreen.alpha = 1;
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("BeforeLevelsScene");
    }
}