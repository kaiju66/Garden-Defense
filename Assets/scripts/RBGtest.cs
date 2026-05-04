using UnityEngine;

[System.Serializable]
public class lootItem
{
    public string name;
    public int chance;
}
public class RNGtest : MonoBehaviour
{
    public lootItem[] loots;

    public void Roll()
    {
        float totalWeight = 0f;
        foreach(var item in loots)
        {
            totalWeight += 1f / item.chance;
        }

        float roll = Random.value * totalWeight;
        foreach(var item in loots)
        {
            float weight = 1f / item.chance;
            if (roll < weight)
            {
                Debug.Log("Випав: " + item.name + " (1/" + item.chance + ")");
                return;
            }
            roll -= weight;
        }
    }
}