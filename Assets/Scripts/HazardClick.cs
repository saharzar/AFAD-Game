using UnityEngine;

public class HazardClick : MonoBehaviour
{
    public GameObject redCircle; // assign in Inspector
    private bool clicked = false;

    public void OnClick()
    {
        if (!clicked)
        {
            clicked = true;
            redCircle.SetActive(true); // show the circle
            GameManager.instance.FoundHazard(); // update counter
        }
    }
}
