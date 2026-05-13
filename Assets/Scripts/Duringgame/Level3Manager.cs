using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Level3Manager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public Sprite image;
    }

    public Question[] questions;

    public Image questionImage;
    public TMP_Text counterText;

    public Sprite congratulationsImage;
    public CanvasGroup imageCanvasGroup;

    public GameObject endButton; // The button that appears at the end

    private int currentIndex = 0;
    private int counter = 0;

    void Start()
    {
        ShowQuestion();
        counterText.text = "0 / " + questions.Length;
        endButton.SetActive(false); // hide button at start
    }

    public void OnAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            counter++;
            counterText.text = counter + " / " + questions.Length;

            currentIndex++;

            if (currentIndex < questions.Length)
            {
                ShowQuestion();
            }
            else
            {
                StartCoroutine(FadeInCongratulations());
            }
        }
        else
        {
            Debug.Log("Wrong answer, try again");
        }
    }

    void ShowQuestion()
    {
        questionImage.sprite = questions[currentIndex].image;
    }

    IEnumerator FadeInCongratulations()
    {
        questionImage.sprite = congratulationsImage;
        counterText.text = "";

        imageCanvasGroup.alpha = 0f;

        float duration = 1.5f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            imageCanvasGroup.alpha = t / duration;
            yield return null;
        }

        imageCanvasGroup.alpha = 1f;

        // Show the button now
        endButton.SetActive(true);
    }
}
