using UnityEngine;

public class TIcksClicks : MonoBehaviour
{

    public GameObject redCircle; // assign in Inspector
    public bool isCorrect = false; // mark correct spots in Inspector
    private bool clicked = false;

    void Start()
    {
        // Automatically find AlanGameManager if not assigned yet
        if (AlanGameManager.instance == null)
        {
            AlanGameManager.instance = FindObjectOfType<AlanGameManager>();
            Debug.Log("AlanGameManager instance auto-assigned: " + AlanGameManager.instance);
        }
    }

    public void OnClick()
    {
        Debug.Log("Button clicked: " + gameObject.name);
        Debug.Log("RedCircle reference: " + redCircle);
        Debug.Log("AlanGameManager instance: " + AlanGameManager.instance);

        if (!clicked)
        {
            clicked = true;

            if (redCircle != null)
            {
                redCircle.SetActive(true);
            }
            else
            {
                Debug.LogWarning("RedCircle is not assigned for " + gameObject.name);
            }

            if (isCorrect && AlanGameManager.instance != null)
            {
                AlanGameManager.instance.FoundHazard(); // only count correct clicks
            }
            else if (isCorrect && AlanGameManager.instance == null)
            {
                Debug.LogError("AlanGameManager instance is missing!");
            }
        }
    }

}
