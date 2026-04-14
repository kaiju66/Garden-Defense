using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public static PlantManager instance;

    public Plant selectedPlant;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("PlantManager створено");
        }
        else
        {
            Debug.LogWarning("Дублікат PlantManager знищено");
            Destroy(gameObject);
        }
    }

    public void SetSelectedPlant(Plant plant)
    {
        if (plant == null)
        {
            Debug.LogError("SetSelectedPlant: plant = NULL");
            return;
        }

        selectedPlant = plant;
        Debug.Log("Вибрано рослину: " + plant.plantName);
    }

    public void ClearSelectedPlant()
    {
        selectedPlant = null;
        Debug.Log("Вибір скинуто");
    }
}