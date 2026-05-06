using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldSelectManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button btnOncesi;
    public Button btnAninda;
    public Button btnSonrasi;

    [Header("Scene Names")]
    public string oncesiScene = "SampleScene";
    public string anindaScene = "AnindaScene";
    public string sonrasiScene = "SonrasiScene";

    void Start()
    {
        btnOncesi.onClick.AddListener(() => LoadWorld(oncesiScene));
        btnAninda.onClick.AddListener(() => LoadWorld(anindaScene));
        btnSonrasi.onClick.AddListener(() => LoadWorld(sonrasiScene));
    }

    void LoadWorld(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning($"Scene '{sceneName}' not in Build Profiles yet!");
        }
    }
}