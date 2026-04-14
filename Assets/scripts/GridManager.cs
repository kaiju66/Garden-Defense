using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 7;
    public int columns = 12;
    public GameObject tilePrefab; 
    public float tileSpacing = 1.1f;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                Vector2 position = new Vector2(c - 2, -r + 6);
                Instantiate(tilePrefab, position, Quaternion.identity);
            }
        }
    }
}