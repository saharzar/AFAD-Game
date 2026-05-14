using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FireGameManager : MonoBehaviour
{
    public Sprite[] questionImages;        // All question pictures
    public Image displayImage;             // The UI Image that shows them

    public Sprite congratulationsImage;    // Final congratulations picture
    public Sprite oneStarImage;            // "You got 1 star" picture

    public CanvasGroup imageCanvasGroup;   // For fade effect

    public GameObject goBackButton;        // Only visible on first question
    public GameObject endButton;           // Appears after congratulations
    public GameObject backToMenuButton;    // Appears after 1-star screen

    private int currentIndex = 0;

    void Start()
    {
        ShowQuestion();

        // Hide buttons at start
        endButton.SetActive(false);
        backToMenuButton.SetActive(false);
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

        // Show the End button now
        endButton.SetActive(true);
    }

    // Called when kid clicks the End Button
    public void ShowOneStarScreen()
    {
        endButton.SetActive(false);
        displayImage.sprite = oneStarImage;
        backToMenuButton.SetActive(true);
    }

    // Called when kid clicks Back to Menu
    public void GoBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
