using UnityEngine;

[CreateAssetMenu(fileName = "NewPlant", menuName = "Plants/Plant")]
public class Plant : ScriptableObject
{
    public string plantName;        // Назва рослини
    public int cost;                // Ціна посадки
    public GameObject plantPrefab;  // Префаб рослини
    public Sprite icon;
    public float cooldown = 5f;

     [Header("Листя")] 
    public bool producesLeaves;     // Чи дає листя
    public int leavesAmount = 1;    // Скільки листя дає за один цикл
    public float produceInterval = 5f; // Через скільки секунд дає листя
    public GameObject LeafPrefab;
    
     [Header("стрільба")] 
    public bool shoot;
    public GameObject[] bulletPrefab;
    public float shootDelay;
    public float range;
    
     [Header("здоров'я")]
    public int maxHealth;

}