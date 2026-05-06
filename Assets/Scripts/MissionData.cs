using UnityEngine;

[System.Serializable]
public class MissionData
{
    public string missionName;        // e.g. "Evde Tehlikeleri Bul"
    public string missionDescription;  // e.g. "Evdeki tehlikeleri öğrenelim."
    public Sprite missionIcon;         // Drag your icon here in Inspector
    public string sceneToLoad;         // Scene name for this mini-game
}
