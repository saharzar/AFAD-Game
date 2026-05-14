using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FloodGameManager : MonoBehaviour
{
    public Sprite[] questionImages;
    public Image displayImage;
    public Sprite congratulationsImage;
    public CanvasGroup imageCanvasGroup;
    public GameObject endButton;

    public GameObject goBackButton;       // NEW: Only visible on first question

    private int currentIndex = 0;

    void Start()
    {
        ShowQuestion();
        endButton.SetActive(false);

        // Show Go Back button at the start (first question)
        goBackButton.SetActive(true);
    }

    public void OnAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            currentIndex++;

            if (currentIndex < questionImages.Length)
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
        displayImage.sprite = questionImages[currentIndex];

        // Show Go Back button ONLY on the first question
        if (currentIndex == 0)
            goBackButton.SetActive(true);
        else
            goBackButton.SetActive(false);
    }

    IEnumerator FadeInCongratulations()
    {
        displayImage.sprite = congratulationsImage;
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
        endButton.SetActive(true);
    }

    // NEW: Go Back button action
    public void GoBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
