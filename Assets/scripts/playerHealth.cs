using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 5;

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("HP: " + health);

        if (health <= 0)
        {
            Debug.Log("GAME OVER");
            // тут потім зробимо меню програшу
        }
    }
}