using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BeforeDisasterManager : MonoBehaviour
{
    public static BeforeDisasterManager Instance;

    [Header("Mission Setup")]
    public MissionData[] missions;
    public MissionButton[] missionButtons;

    [Header("Progress UI")]
    public Slider progressBar;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI badgeText;

    [Header("Reward Popup")]
    public RewardPopup rewardPopup;

    [Header("Navigation")]
    public string mainMenuScene = "MainMenu";

    private const string SAVE_KEY = "BeforeDisaster_Progress";
    private int _completedCount;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Remove this line when done testing!
        // PlayerPrefs.SetInt("BeforeDisaster_Progress", 2);

        _completedCount = PlayerPrefs.GetInt(SAVE_KEY, 0);
        RefreshUI();
    }

    void OnEnable()
    {
        _completedCount = PlayerPrefs.GetInt(SAVE_KEY, 0);
        RefreshUI();

        if (missions != null && _completedCount >= missions.Length)
        {
            if (rewardPopup != null)
                rewardPopup.Show();
        }
    }

    void RefreshUI()
    {
        // Safety check
        if (missions == null || missionButtons == null) return;

        for (int i = 0; i < missionButtons.Length; i++)
        {
            if (missionButtons[i] == null || i >= missions.Length) continue;

            bool completed = i < _completedCount;
            bool unlocked = i <= _completedCount;
            missionButtons[i].Setup(missions[i], i, completed, unlocked);
        }

        if (progressBar != null)
        {
            float pct = (float)_completedCount / missions.Length;
            progressBar.value = pct;

            int pctInt = Mathf.RoundToInt(pct * 100);

            if (progressText != null)
                progressText.text = $"Kahramanlik Seviyesi: %{pctInt}";

            if (badgeText != null)
                badgeText.text = $"{_completedCount} / {missions.Length}";
        }
    }

    public void OnMissionClicked(int index, string sceneName)
    {
        PlayerPrefs.SetInt("CurrentMissionIndex", index);
        PlayerPrefs.Save();
        SceneManager.LoadScene(sceneName);
    }

    public static void CompleteMission(int missionIndex)
    {
        int current = PlayerPrefs.GetInt("BeforeDisaster_Progress", 0);
        if (missionIndex >= current)
        {
            PlayerPrefs.SetInt("BeforeDisaster_Progress", missionIndex + 1);
            PlayerPrefs.Save();
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}