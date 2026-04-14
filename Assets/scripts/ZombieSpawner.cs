using UnityEngine;
using TMPro;

[System.Serializable]
public class ZombieType
{
    public GameObject prefab;
    public float spawnChance;
}

[System.Serializable]
public class Wave
{
    public ZombieType[] zombies;

    public int zombieCount = 20;
    public float spawnRate = 2f;
}

public class ZombieSpawner : MonoBehaviour
{
    public Wave[] waves;

    public int rows = 7;
    public float cellHeight = 1f;
    public float spawnX = 12f;

    public float startDelay = 8f;
    public float breakTime = 5f;

    [Header("UI")]
    public TextMeshProUGUI waveText;
    public float waveTextDuration = 2f;

    private int currentWaveIndex = 0;

    private int zombiesToSpawn;
    private int zombiesAlive;

    private float timer;
    private bool isWaveActive = false;

    void Start()
    {
        Invoke(nameof(StartWave), startDelay);
    }

    void Update()
    {
        if (!isWaveActive) return;

        timer += Time.deltaTime;

        Wave currentWave = waves[currentWaveIndex];

        if (timer >= currentWave.spawnRate && zombiesToSpawn > 0)
        {
            SpawnZombie(currentWave);
            timer = 0f;
        }

        if (zombiesAlive <= 0 && zombiesToSpawn <= 0)
        {
            EndWave();
        }
    }

    void StartWave()
    {
        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("ВСІ ХВИЛІ ЗАВЕРШЕНО!");
            return;
        }

        Wave wave = waves[currentWaveIndex];

        zombiesToSpawn = wave.zombieCount;
        zombiesAlive = 0;

        ShowWaveText(currentWaveIndex + 1);

        Invoke(nameof(ActivateWave), waveTextDuration);
    }

    void ActivateWave()
    {
        isWaveActive = true;
        Debug.Log("Хвиля " + (currentWaveIndex + 1) + " почалась!");
    }

    void EndWave()
    {
        isWaveActive = false;

        Debug.Log("Хвиля завершена!");

        Invoke(nameof(NextWave), breakTime);
    }

    void NextWave()
    {
        currentWaveIndex++;
        StartWave();
    }

    void SpawnZombie(Wave wave)
    {
        int randomRow = Random.Range(0, rows);
        float yPos = randomRow * cellHeight;

        Vector3 spawnPos = new Vector3(spawnX, yPos, 0);

        GameObject prefab = GetRandomZombie(wave);

        GameObject zombie = Instantiate(prefab, spawnPos, Quaternion.identity);

        zombiesToSpawn--;
        zombiesAlive++;

        zombie.GetComponent<Zombie>().spawner = this;
    }

    public void ZombieDied()
    {
        zombiesAlive--;
    }

    GameObject GetRandomZombie(Wave wave)
    {
        float total = 0;

        foreach (var z in wave.zombies)
            total += z.spawnChance;

        float rand = Random.Range(0, total);

        foreach (var z in wave.zombies)
        {
            if (rand < z.spawnChance)
                return z.prefab;

            rand -= z.spawnChance;
        }

        return wave.zombies[0].prefab;
    }

    void ShowWaveText(int waveNumber)
    {
        if (waveText == null) return;

        waveText.gameObject.SetActive(true);
        waveText.text = "Хвиля " + waveNumber;

        CancelInvoke(nameof(HideWaveText));
        Invoke(nameof(HideWaveText), waveTextDuration);
    }

    void HideWaveText()
    {
        if (waveText == null) return;

        waveText.gameObject.SetActive(false);
    }
}