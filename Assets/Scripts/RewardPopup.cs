using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class RewardPopup : MonoBehaviour
{
    [Header("Popup UI")]
    public GameObject popupPanel;      // The whole popup panel
    public TextMeshProUGUI titleText;       // "Harika!"
    public TextMeshProUGUI messageText;     // Mascot speech bubble text
    public GameObject starObject;      // The big star
    public ParticleSystem confettiEffect;  // Confetti particles
    public Button closeButton;     // Continue / close button

    void Start()
    {
        popupPanel.SetActive(false);
        closeButton.onClick.AddListener(Hide);
    }

    public void Show()
    {
        popupPanel.SetActive(true);
        titleText.text = "Harika! ??";
        messageText.text = "Tüm tehlikeleri buldun!\nEvde güvende olmak\nçok önemli! ??";

        if (confettiEffect != null)
            confettiEffect.Play();

        // Animate the star with a pop effect
        StartCoroutine(AnimateStar());
    }

    public void Hide()
    {
        popupPanel.SetActive(false);
    }

    IEnumerator AnimateStar()
    {
        starObject.transform.localScale = Vector3.zero;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            float scale = Mathf.SmoothStep(0f, 1f, t);
            starObject.transform.localScale = Vector3.one * scale;
            yield return null;
        }
        starObject.transform.localScale = Vector3.one;
    }
}
