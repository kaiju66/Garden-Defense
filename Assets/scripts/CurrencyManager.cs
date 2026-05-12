using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public int LeafBalance { get; private set; } = 0;
    void Awake()
    {
        Instance = this;
    }

   
    void Start()
    {
        AddLeaves(200);
        Invoke("TimerLeaves", 15f);
    }

    public void AddLeaves(int amount)
    {
        LeafBalance += amount;
        Debug.Log("Leaves added: " + amount + ". Total: " + LeafBalance);
    }

    public bool SpendLeaves(int amount)
    {
        if (LeafBalance >= amount)
        {
            LeafBalance -= amount;
            return true;
        }
        return false;
    }

    public void TimerLeaves()
    {
        AddLeaves(25);
    }
}