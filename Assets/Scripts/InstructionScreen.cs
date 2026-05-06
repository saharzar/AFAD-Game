using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionScreen : MonoBehaviour
{
    [Header("Settings")]
    public string gameSceneName = "SampleScene"; // scene to load when Play is clicked

    // Assign your Play button in Inspector and connect onClick to this
    public void OnPlayClicked()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    // Optional: back button to return to mission select
    public void OnBackClicked()
    {
        SceneManager.LoadScene("BeforeLevelsScene");
    }
}