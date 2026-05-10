using UnityEngine;
using TMPro;
using System.Collections;

public class AlanGameManager : MonoBehaviour
{
    public static AlanGameManager instance;
    public TMP_Text counterText;
    public GameObject congratulationsScreen; // assign in Inspector
    private CanvasGroup congratsCanvasGroup;

    private int foundHazards = 0;
    private int maxHazards = 3; // only 3 correct answers needed

    void Awake()
    {
        instance = this;
        congratsCanvasGroup = congratulationsScreen.GetComponent<CanvasGroup>();
    }

    public void FoundHazard()
    {
        foundHazards++;
        counterText.text = foundHazards + "/" + maxHazards;

        if (foundHazards == maxHazards)
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
