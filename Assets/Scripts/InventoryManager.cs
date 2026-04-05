using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public Transform[] slots;
    public Transform canvasRoot;

    [Header("Ready Button & Result")]
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public Button playAgainButton;

    // Rules
    private List<string> gameItems = new List<string> { "Bear", "Ball", "Cards" };
    private List<string> forbiddenItems = new List<string> { "Books" };
    private List<string> essentialItems = new List<string>
    {
        "Torch", "Water", "Food", "Kit", "Clothes", "Blanket", "Whistle"
    };

    // This is the REAL tracked list
    private List<string> collectedItemNames = new List<string>();

    void Start()
    {
        resultPanel.SetActive(false);
        playAgainButton.onClick.AddListener(OnPlayAgain);
    }

    public bool AddItem(GameObject item)
    {
        // Check if slots are full
        bool hasEmptySlot = false;
        foreach (Transform slot in slots)
        {
            if (slot.childCount == 0)
            {
                hasEmptySlot = true;
                break;
            }
        }

        if (!hasEmptySlot)
        {
            Debug.Log("Inventory Full!");
            return false;
        }

        string itemName = item.GetComponent<Item>().itemName;

        // Add to tracked list
        collectedItemNames.Add(itemName);
        Debug.Log("Added: " + itemName + " | Total items: " + collectedItemNames.Count);

        foreach (Transform slot in slots)
        {
            if (slot.childCount == 0)
            {
                StartCoroutine(MoveToSlot(item, slot));
                return true;
            }
        }

        return false;
    }

    public void RemoveItem(string itemName)
    {
        collectedItemNames.Remove(itemName);
        Debug.Log("Removed: " + itemName + " | Total items: " + collectedItemNames.Count);
    }

    public void OnReadyClicked()
    {
        Debug.Log("Ready clicked! Items in bag: " + string.Join(", ", collectedItemNames));
        resultText.text = EvaluateBag();
        resultPanel.SetActive(true);
    }

    string EvaluateBag()
    {
        List<string> gotItems = new List<string>();       // good items they picked
        List<string> problems = new List<string>();       // wrong items
        List<string> missing = new List<string>();        // essentials they missed

        int gameCount = 0;
        string pickedGameItem = "";

        foreach (string name in collectedItemNames)
        {
            if (gameItems.Contains(name))
            {
                gameCount++;
                pickedGameItem = name;
            }

            if (forbiddenItems.Contains(name))
            {
                problems.Add(name + " (not useful in emergencies!)");
            }
            else if (!gameItems.Contains(name) || gameCount == 1)
            {
                // It's a good/essential item OR the first game item
                if (!problems.Contains(name))
                    gotItems.Add(name);
            }
        }

        // Too many games
        if (gameCount > 1)
            problems.Add("You picked " + gameCount + " toys/games - only 1 is allowed!");

        // Check missing essentials
        foreach (string essential in essentialItems)
        {
            if (!collectedItemNames.Contains(essential))
                missing.Add(essential);
        }

        // ---- Build the message ----
        string msg = "";

        // What they got right
        if (gotItems.Count > 0)
        {
            msg += "? You got:\n";
            foreach (string g in gotItems)
                msg += "  • " + g + "\n";
            msg += "\n";
        }

        // Problems
        if (problems.Count > 0)
        {
            msg += "? Uh oh! Remove these:\n";
            foreach (string p in problems)
                msg += "  • " + p + "\n";
            msg += "\n";
        }

        // Missing essentials
        if (missing.Count > 0)
        {
            msg += "! Don't forget:\n";
            foreach (string m in missing)
                msg += "  • " + m + "\n";
        }

        // Perfect bag!
        if (problems.Count == 0 && missing.Count == 0 && collectedItemNames.Count > 0)
        {
            msg = "PERFECT! ??\n\nYour emergency bag is ready!\n\nYou got:\n";
            foreach (string g in collectedItemNames)
                msg += "  • " + g + "\n";
        }

        // Nothing selected
        if (collectedItemNames.Count == 0)
        {
            msg = "Your bag is empty!\nPick some items from the room first.";
        }

        return msg;
    }

    void OnPlayAgain()
    {
        // Force return all items
        foreach (Transform slot in slots)
        {
            if (slot.childCount > 0)
            {
                Item item = slot.GetComponentInChildren<Item>();
                if (item != null)
                    item.ForceReturn();
            }
        }

        collectedItemNames.Clear();
        resultPanel.SetActive(false);
    }

    IEnumerator MoveToSlot(GameObject item, Transform slot)
    {
        RectTransform rect = item.GetComponent<RectTransform>();
        Vector3 startPos = rect.position;
        Vector3 endPos = slot.position;
        float duration = 0.5f;
        float time = 0;

        item.transform.SetParent(canvasRoot, true);

        while (time < duration)
        {
            rect.position = Vector3.Lerp(startPos, endPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        item.transform.SetParent(slot, false);
        rect.localPosition = Vector3.zero;
    }
}