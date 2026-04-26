using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [Header("References")]
    public Button startButton;
    public CanvasGroup canvasGroup;

    [Header("Settings")]
    public string nextSceneName = "WorldSelect";
    public float fadeInDuration = 0.8f;
    public float fadeOutDuration = 0.5f;

    void Start()
    {
        startButton.onClick.AddListener(OnStartClicked);
        StartCoroutine(FadeIn());
    }

    void OnStartClicked()
    {
        startButton.interactable = false;
        StartCoroutine(FadeOutAndLoad());
    }

    IEnumerator FadeIn()
    {
        canvasGroup.alpha = 0f;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeInDuration;
            canvasGroup.alpha = Mathf.Clamp01(t);
            yield return null;
        }
    }

    IEnumerator FadeOutAndLoad()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeOutDuration;
            canvasGroup.alpha = 1f - Mathf.Clamp01(t);
            yield return null;
        }
        SceneManager.LoadScene(nextSceneName);
    }
}