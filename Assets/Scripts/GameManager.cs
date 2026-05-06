using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMP_Text counterText;
    public GameObject congratulationsScreen; // assign in Inspector
    private CanvasGroup congratsCanvasGroup;

    private int foundHazards = 0;

    void Awake()
    {
        instance = this;
        congratsCanvasGroup = congratulationsScreen.GetComponent<CanvasGroup>();
    }

    public void FoundHazard()
    {
        foundHazards++;
        counterText.text = foundHazards + "/5";

        if (foundHazards == 5)
        {
            StartCoroutine(FadeInCongratulations());
        }
    }

    IEnumerator FadeInCongratulations()
    {
        congratulationsScreen.SetActive(true);

        float duration = 1.5f; // seconds
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            congratsCanvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
    }
}

