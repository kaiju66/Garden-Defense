using UnityEngine;

public class PlantHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private Tile tile; // 👈 додаємо

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void SetTile(Tile t)
    {
        tile = t;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            // 👇 звільняємо клітинку
            if (tile != null)
            {
                tile.isOccupied = false;
            }

            Destroy(gameObject);
        }
    }
}