using UnityEngine;

public class PlantBehaviour : MonoBehaviour
{
    public Plant data; // 👈 посилання на ScriptableObject

    private SpriteRenderer sr;
    private Color originalColor;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    public void Highlight(bool isOn)
    {
        if (isOn)
            sr.color = Color.red;
        else
            sr.color = originalColor;
    }

    public int GetSellValue()
    {
        return data.cost / 2;
    }
}