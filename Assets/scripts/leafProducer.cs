using UnityEngine;

public class LeafProducer : MonoBehaviour
{
    public int leafAmount = 25; // скільки валюти давати за раз
    public float delay = 10f;   // інтервал отримання валюти

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delay)
        {
            GiveLeaves();
            timer = 0f;
        }
    }

    void GiveLeaves()
    {
        // Додаємо валюту гравцю
        CurrencyManager.Instance.AddLeaves(leafAmount); 
        // Можна вивести для тесту
        Debug.Log("Leaves added: " + leafAmount + ". Total: " + CurrencyManager.Instance.LeafBalance);
    }
}