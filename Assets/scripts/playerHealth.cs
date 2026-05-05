using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 5;
    public Image[] hearts;
    public GameObject GameOverP;

    void Start()
    {
        GameOverP.SetActive(false);
        UpdateHearts();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        UpdateHearts();

        Debug.Log("HP: " + health);

        if (health <= 0)
        {
            Time.timeScale = 0f;
            GameOverP.SetActive(true);
        }   
    }

    void UpdateHearts()
    {
        for(int i = 0;i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].enabled = true;
            }

            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}