using UnityEngine;
using UnityEngine.InputSystem;

public class PlantPlacer : MonoBehaviour
{
    public Plant selectedPlant;
    private PlantButton selectedButton;

    private GameObject previewPlant;
    private SpriteRenderer previewRenderer;

    void Update()
    {
        if (selectedPlant == null)
        {
            ClearPreview();
            return;
        }

        if (Camera.main == null) return;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0;

        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);

        Tile currentTile = null;
        Vector3 targetPos = mouseWorldPos;
        bool canPlace = false;

        if (hit != null)
        {
            currentTile = hit.GetComponent<Tile>();

            if (currentTile != null)
            {
                targetPos = currentTile.transform.position;

                if (!currentTile.isOccupied &&
                    CurrencyManager.Instance != null &&
                    CurrencyManager.Instance.LeafBalance >= selectedPlant.cost)
                {
                    canPlace = true;
                }
            }
        }

        // створення прев'ю
        if (previewPlant == null)
        {
            previewPlant = Instantiate(selectedPlant.plantPrefab);
            previewRenderer = previewPlant.GetComponent<SpriteRenderer>();

            // ❗ ВИМКНУТИ ВСІ СКРИПТИ (щоб не стріляло)
            MonoBehaviour[] scripts = previewPlant.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                script.enabled = false;
            }

            // ❗ вимкнути колайдер
            Collider2D col = previewPlant.GetComponent<Collider2D>();
            if (col != null) col.enabled = false;

            // прозорість
            if (previewRenderer != null)
            {
                Color c = previewRenderer.color;
                c.a = 0.5f;
                previewRenderer.color = c;
            }
        }

        previewPlant.transform.position = targetPos;

        if (previewRenderer != null)
        {
            previewRenderer.color = canPlace
                ? new Color(0, 1, 0, 0.5f)
                : new Color(1, 0, 0, 0.5f);
        }

        // посадка
        if (Mouse.current.leftButton.wasReleasedThisFrame && canPlace && currentTile != null)
        {
            if (CurrencyManager.Instance.SpendLeaves(selectedPlant.cost))
            {
                GameObject plant = Instantiate(selectedPlant.plantPrefab, targetPos, Quaternion.identity);

                currentTile.isOccupied = true;

                PlantHealth ph = plant.GetComponent<PlantHealth>();
                if (ph != null)
                    ph.SetTile(currentTile);

                // ✅ запускаємо cooldown
                if (selectedButton != null)
                    selectedButton.StartCooldown();

                // ✅ очищаємо після 1 посадки
                selectedPlant = null;
                selectedButton = null;
                ClearPreview();
            }
        }

        // ПКМ скасування
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            selectedPlant = null;
            selectedButton = null;
            ClearPreview();
        }
    }

    public void SelectPlant(Plant plant, PlantButton button)
    {
        selectedPlant = plant;
        selectedButton = button;

        ClearPreview();
    }

    void ClearPreview()
    {
        if (previewPlant != null)
        {
            Destroy(previewPlant);
            previewPlant = null;
        }
    }
}