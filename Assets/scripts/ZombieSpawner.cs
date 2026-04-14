using UnityEngine;

[System.Serializable]
public class ZombieType
{
    public GameObject prefab;
    public float spawnChance;
}

public class ZombieSpawner : MonoBehaviour
{
    public ZombieType[] zombies;

    public int rows = 7;
    public float cellHeight = 1f;
    public float spawnX = 12f;

    public float spawnRate = 2f;

    public int startWaveSize = 20;
    public float breakTime = 8f;

    private int currentWave = 1;
    private int zombiesToSpawn;
    private int zombiesAlive;

    private float timer;
    private bool isWaveActive = false;

    void Start()
    {
        StartWave();
    }

    void Update()
    {
        if (!isWaveActive) return;

        timer += Time.deltaTime;

        if (timer >= spawnRate && zombiesToSpawn > 0)
        {
            SpawnZombie();
            timer = 0f;
        }

        // якщо всі зомбі закінчились
        if (zombiesAlive <= 0 && zombiesToSpawn <= 0)
        {
            EndWave();
        }
    }

    void StartWave()
    {
        isWaveActive = true;

        zombiesToSpawn = startWaveSize + (currentWave * 5);
        zombiesAlive = 0;

        Debug.Log("Хвиля " + currentWave + " почалась!");
    }

    void EndWave()
    {
        isWaveActive = false;

        Debug.Log("Хвиля закінчена!");

        Invoke(nameof(NextWave), breakTime);
    }

    void NextWave()
    {
        currentWave++;
        StartWave();
    }

    void SpawnZombie()
    {
        int randomRow = Random.Range(0, rows);
        float yPos = randomRow * cellHeight;

        Vector3 spawnPos = new Vector3(spawnX, yPos, 0);

        GameObject zombiePrefab = GetRandomZombie();

        GameObject zombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);

        zombiesToSpawn--;
        zombiesAlive++;

        zombie.GetComponent<Zombie>().spawner = this;
    }

    public void ZombieDied()
    {
        zombiesAlive--;
    }

    GameObject GetRandomZombie()
    {
        float total = 0;

        foreach (var z in zombies)
            total += z.spawnChance;

        float rand = Random.Range(0, total);

        foreach (var z in zombies)
        {
            if (rand < z.spawnChance)
                return z.prefab;

            rand -= z.spawnChance;
        }

        return zombies[0].prefab;
    }
}