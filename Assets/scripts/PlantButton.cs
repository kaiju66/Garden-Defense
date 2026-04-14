using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantButton : MonoBehaviour
{
    [Header("UI")]
    public Image icon;
    public TextMeshProUGUI costText;
    public Image cooldownOverlay;

    [Header("Data")]
    public Plant plant;
    public PlantPlacer plantPlacer;

    private float currentCooldown = 0f;
    private bool isCooldown = false;

    void Awake()
    {
        if (icon == null)
            icon = GetComponentInChildren<Image>();

        if (costText == null)
            costText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        icon.sprite = plant.icon;
        costText.text = plant.cost.ToString();

        if (cooldownOverlay != null)
            cooldownOverlay.fillAmount = 0f;
    }

    void Update()
    {
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;

            costText.text = Mathf.Ceil(currentCooldown).ToString();

            if (cooldownOverlay != null)
                cooldownOverlay.fillAmount = currentCooldown / plant.cooldown;

            if (currentCooldown <= 0f)
            {
                isCooldown = false;
                costText.text = plant.cost.ToString();

                if (cooldownOverlay != null)
                    cooldownOverlay.fillAmount = 0f;
            }
        }
    }

    public void OnClick()
    {
        if (isCooldown) return;

        plantPlacer.SelectPlant(plant, this);
    }

    public void StartCooldown()
    {
        isCooldown = true;
        currentCooldown = plant.cooldown;
    }
}