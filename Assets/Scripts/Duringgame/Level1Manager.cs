using UnityEngine;
using TMPro;

public class Level1Manager : MonoBehaviour
{
    public GameObject actionOrderPanel;
    public TMP_Text feedbackText;

    public GameObject safeTick;
    public GameObject wrongTick1, wrongTick2, wrongTick3;

    public RectTransform cokButton, kapanButton, tutunButton;
    private Vector2 cokStartPos, kapanStartPos, tutunStartPos;

    public GameObject nextLevelButton; // drag your button here

    private string[] correctOrder = { "Çök", "Kapan", "Tutun" };
    private string[] chosenOrder = new string[3];

    private void Start()
    {
        actionOrderPanel.SetActive(false);
        safeTick.SetActive(false);
        wrongTick1.SetActive(false);
        wrongTick2.SetActive(false);
        wrongTick3.SetActive(false);

        cokStartPos = cokButton.anchoredPosition;
        kapanStartPos = kapanButton.anchoredPosition;
        tutunStartPos = tutunButton.anchoredPosition;

        nextLevelButton.SetActive(false); // hide at start
    }

    public void SafeSpotClicked()
    {
        feedbackText.text = "Correct! You chose the safe spot!";
        safeTick.SetActive(true);
        actionOrderPanel.SetActive(true);
    }

    public void WrongSpot1Clicked() { feedbackText.text = "Wrong! That place is unsafe."; wrongTick1.SetActive(true); }
    public void WrongSpot2Clicked() { feedbackText.text = "Wrong! That place is unsafe."; wrongTick2.SetActive(true); }
    public void WrongSpot3Clicked() { feedbackText.text = "Wrong! That place is unsafe."; wrongTick3.SetActive(true); }

    public void AddAction(string actionName, int slotIndex)
    {
        chosenOrder[slotIndex] = actionName;

        bool allFilled = true;
        foreach (string s in chosenOrder)
            if (string.IsNullOrEmpty(s)) { allFilled = false; break; }

        if (allFilled) CheckOrder();
    }

    private void CheckOrder()
    {
        for (int i = 0; i < correctOrder.Length; i++)
        {
            if (chosenOrder[i] != correctOrder[i])
            {
                feedbackText.text = "Wrong order! Try again!";
                ResetButtons();
                chosenOrder = new string[3];
                return;
            }
        }

        feedbackText.text = "Great! You placed them in the right order!";
        nextLevelButton.SetActive(true); // show the button
    }

    private void ResetButtons()
    {
        cokButton.anchoredPosition = cokStartPos;
        kapanButton.anchoredPosition = kapanStartPos;
        tutunButton.anchoredPosition = tutunStartPos;
    }

    // Called when Next Level button is clicked
    public void GoToNextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level2_Classroom");
    }
}
