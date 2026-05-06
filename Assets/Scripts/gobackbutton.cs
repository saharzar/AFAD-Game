using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    [SerializeField] string targetScene;

    public void GoBack()
    {
        SceneManager.LoadScene(targetScene);
    }
}
