using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("References")]
    public Button startButton;

    [Header("Settings")]
    public string nextSceneName = "WorldSelect";

    void Start()
    {
        startButton.onClick.AddListener(OnStartClicked);
    }

    void OnStartClicked()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}