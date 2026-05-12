using UnityEngine;

public class DifficultyChanger : MonoBehaviour
{
    public void Easy()
    {
        GameSetting.difficulty = 0.75f;
        Debug.Log("Difficalty Easy");
    }

    public void Hard()
    {
        GameSetting.difficulty = 1.25f;
        Debug.Log("Difficalty Hard");
    }
    
    public void Normal()
    {
        GameSetting.difficulty = 1f;
        Debug.Log("Difficalty Normal");
    }

    public void Imposible()
    {
        GameSetting.difficulty = 1.5f;
        Debug.Log("Difficalty Imposible");
    }
}