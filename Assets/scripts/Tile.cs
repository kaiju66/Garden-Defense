using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupied = false;

    public void SetOccupied(bool value)
    {
        isOccupied = value;
    }
}