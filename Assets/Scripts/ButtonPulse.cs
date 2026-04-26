using UnityEngine;

public class ButtonPulse : MonoBehaviour
{
    [Header("Pulse Settings")]
    public float minScale = 0.96f;
    public float maxScale = 1.04f;
    public float speed = 1.8f;

    private RectTransform rt;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        float scale = Mathf.Lerp(minScale, maxScale,
                      (Mathf.Sin(Time.time * speed) + 1f) * 0.5f);
        rt.localScale = Vector3.one * scale;
    }
}