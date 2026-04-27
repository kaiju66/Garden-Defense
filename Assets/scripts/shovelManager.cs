using UnityEngine;
using UnityEngine.InputSystem;

public class ShovelManager : MonoBehaviour
{
    public bool shovelMode = false;
    public LayerMask plantLayer;

    private PlantBehaviour currentPlant;

    void Update()
    {
        HandleCancel();

        if (!shovelMode) return;

        HandleHover();
        HandleClick();
    }

    void HandleCancel()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            shovelMode = false;
            ClearHighlight();
        }
    }

    void HandleHover()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, plantLayer);

        if (hit.collider != null)
        {
            PlantBehaviour plant = hit.collider.GetComponent<PlantBehaviour>();

            if (currentPlant != plant)
            {
                ClearHighlight();
                currentPlant = plant;
                currentPlant.Highlight(true);
            }
        }
        else
        {
            ClearHighlight();
        }
    }

    void HandleClick()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && currentPlant != null)
        {
            int refund = currentPlant.GetSellValue();

            if (CurrencyManager.Instance != null)
            {
                CurrencyManager.Instance.AddLeaves(refund);
            }

            Destroy(currentPlant.gameObject);
            currentPlant = null;
        }
    }

    void ClearHighlight()
    {
        if (currentPlant != null)
        {
            currentPlant.Highlight(false);
            currentPlant = null;
        }
    }

    public void ToggleShovel()
    {
        shovelMode = !shovelMode;
        ClearHighlight();
    }
}