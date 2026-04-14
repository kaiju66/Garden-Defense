using UnityEngine;
using TMPro;

public class CurrencyUI : MonoBehaviour
{
    public TextMeshProUGUI leafText;

    void Update()
    {
        if (CurrencyManager.Instance != null)
        {
            leafText.text = "Leaves: " + CurrencyManager.Instance.LeafBalance;
        }
    }
}