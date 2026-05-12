using UnityEngine;
using TMPro;

public class Level2Manager : MonoBehaviour
{
    public TMP_Text feedbackText;
    public GameObject nextLevelButton;

    [Header("Icons per Spot")]
    public GameObject teacherDeskTick;
    public GameObject frontDeskTick;
    public GameObject doorCross;
    public GameObject bookshelfCross;
    public GameObject windowCross;

    private int safeChoices = 0;

    private void Start()
    {
        feedbackText.text = "Deprem başladı! En güvenli sırayı seç!";
        nextLevelButton.SetActive(false);
        HideAllIcons();
    }

    private void HideAllIcons()
    {
        if (teacherDeskTick) teacherDeskTick.SetActive(false);
        if (frontDeskTick) frontDeskTick.SetActive(false);
        if (doorCross) doorCross.SetActive(false);
        if (bookshelfCross) bookshelfCross.SetActive(false);
        if (windowCross) windowCross.SetActive(false);
    }

    public void TeacherDeskClicked()
    {
        feedbackText.text = "Harika! Masanın altı güvenli bir yerdir.";
        if (teacherDeskTick) teacherDeskTick.SetActive(true);
        safeChoices++;
        CheckWin();
    }

    public void FrontDeskClicked()
    {
        feedbackText.text = "Doğru! Masanın altı seni korur.";
        if (frontDeskTick) frontDeskTick.SetActive(true);
        safeChoices++;
        CheckWin();
    }

    public void DoorClicked()
    {
        feedbackText.text = "Pencere kenarı tehlikelidir! Cam kırılabilir.";
        if (doorCross) doorCross.SetActive(true);
    }

    public void BookshelfClicked()
    {
        feedbackText.text = "Kitaplık tehlikelidir! Üzerine eşyalar düşebilir.";
        if (bookshelfCross) bookshelfCross.SetActive(true);
    }

    public void WindowClicked()
    {
        feedbackText.text = "Pencere kenarı tehlikelidir! Cam kırılabilir.";
        if (windowCross) windowCross.SetActive(true);
    }

    private void CheckWin()
    {
        if (safeChoices >= 2)
        {
            feedbackText.text = "Tebrikler! En güvenli yerleri buldun!";
            nextLevelButton.SetActive(true);
        }
    }

    public void GoToNextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level3_House");
    }
}