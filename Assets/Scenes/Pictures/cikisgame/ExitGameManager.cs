using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;



public class ExitGameManager : MonoBehaviour
{
    public TMP_Text counterText;
    public GameObject congratulationsScreen; // assign in Inspector

    private int removedCount = 0;
    private int totalObstacles = 5;

    public void RemoveObstacle(Button obstacleButton)
    {
        obstacleButton.gameObject.SetActive(false);
        removedCount++;
        UpdateCounter();

        if (removedCount >= totalObstacles)
        {
            ShowCongratulations();
        }
    }

    private void UpdateCounter()
    {
        counterText.text = removedCount + " / " + totalObstacles;
    }

    private void ShowCongratulations()
    {
        congratulationsScreen.SetActive(true);
        StartCoroutine(FadeInScreen());
    }

    private IEnumerator FadeInScreen()
    {
        CanvasGroup cg = congratulationsScreen.GetComponent<CanvasGroup>();
        if (cg == null)
        {
            cg = congratulationsScreen.AddComponent<CanvasGroup>();
        }

        cg.alpha = 0;
        while (cg.alpha < 1)
        {
            cg.alpha += Time.deltaTime / 1.5f; // fade in over 1.5 seconds
            yield return null;
        }
    }
}
