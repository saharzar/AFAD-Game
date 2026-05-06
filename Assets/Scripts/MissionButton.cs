using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionButton : MonoBehaviour
{
    [Header("UI References")]
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descText;
    public GameObject lockIcon;
    public GameObject starIcon;
    public Button button;
    public Image buttonBackground;

    [Header("Colors")]
    public Color completedColor = new Color(0.4f, 0.8f, 0.2f);
    public Color unlockedColor = new Color(1f, 0.6f, 0.1f);
    public Color lockedColor = new Color(0.5f, 0.5f, 0.5f);

    private MissionData _data;
    private int _index;

    public void Setup(MissionData data, int index, bool isCompleted, bool isUnlocked)
    {
        _data = data;
        _index = index;

        // Null checks for text
        if (nameText != null)
            nameText.text = data.missionName;

        if (descText != null)
            descText.text = data.missionDescription;

        // Only swap sprite if one is actually assigned
        if (iconImage != null && data.missionIcon != null)
            iconImage.sprite = data.missionIcon;

        if (isCompleted)
            SetState_Completed();
        else if (isUnlocked)
            SetState_Unlocked();
        else
            SetState_Locked();
    }

    void SetState_Completed()
    {
        if (buttonBackground != null)
            buttonBackground.color = completedColor;

        if (lockIcon != null) lockIcon.SetActive(false);
        if (starIcon != null) starIcon.SetActive(true);

        if (button != null)
        {
            button.interactable = true;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClick);
        }
    }

    void SetState_Unlocked()
    {
        if (buttonBackground != null)
            buttonBackground.color = unlockedColor;

        if (lockIcon != null) lockIcon.SetActive(false);
        if (starIcon != null) starIcon.SetActive(false);

        if (button != null)
        {
            button.interactable = true;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClick);
        }
    }

    void SetState_Locked()
    {
        if (buttonBackground != null)
            buttonBackground.color = lockedColor;

        if (lockIcon != null) lockIcon.SetActive(true);
        if (starIcon != null) starIcon.SetActive(false);

        if (button != null)
            button.interactable = false;
    }

    void OnClick()
    {
        if (BeforeDisasterManager.Instance != null)
            BeforeDisasterManager.Instance.OnMissionClicked(_index, _data.sceneToLoad);
    }
}