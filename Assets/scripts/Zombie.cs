using UnityEngine;

public class Zombie : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 1f;
    public float startMoveDelay = 0f;
    private bool canMove = false;
    float originalSpeed;

    bool isSlowed = false;
    bool isFreezed = false;
    bool isFired = false;
    float fireDelay = 1f;
    private float timer;
    public int fireDamage = 4;

    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Attack")]
    public int damage = 10;
    public float attackDelay = 1f;
    private float attackTimer;

    [Header("Armor")]
    public int armorHealth = 50;       // 🛡️ хп броні
    public int armorReduction = 5;     // 🛡️ зменшення урону
    public GameObject armorObject;     // 🎨 об'єкт броні (відро)

    [Header("Gameplay")]
    public float destroyX = -2f;

    private bool isAttacking = false;
    private PlantHealth targetPlant;

    private PlayerHealth playerHealth;
    public ZombieSpawner spawner;
    public GameObject iceCube;
    private SpriteRenderer sr;
    

    void Start()
    {
        DifficultyStat();

        currentHealth = maxHealth;

        playerHealth = FindAnyObjectByType<PlayerHealth>();

        Invoke(nameof(EnableMovement), startMoveDelay);

        originalSpeed = speed;
        iceCube.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!canMove) return;

        if (isAttacking)
        {
            Attack();
        }
        else
        {
            Move();
        }
        
        if(isFired)
        {
            timer += Time.deltaTime;
            if(timer >= fireDelay)
            {
                TakeDamage(fireDamage);
                timer = 0f;
            }
        }
    }

    void EnableMovement()
    {
        canMove = true;
    }

    void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void Attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackDelay)
        {
            if (targetPlant != null)
            {
                targetPlant.TakeDamage(damage);
            }

            attackTimer = 0f;
        }
    }

    // 💥 ОТРИМАННЯ УРОНУ
    public void TakeDamage(int dmg)
    {
        int finalDamage = dmg;

        // якщо броня ще є
        if (armorHealth > 0)
        {
            finalDamage = Mathf.Max(dmg - armorReduction, 1);

            armorHealth -= finalDamage;

            if (armorHealth <= 0)
            {
                BreakArmor();
            }
        }

        currentHealth -= finalDamage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 🛡️ ЛОМАННЯ БРОНІ
    void BreakArmor()
    {
        Debug.Log("Броня зламалась!");

        // ❌ вимикаємо відро / броню
        if (armorObject != null)
        {
            armorObject.SetActive(false);
        }

        armorHealth = 0;
    }

    void Die()
    {
        if (spawner != null)
        {
            spawner.ZombieDied();
        }

        Destroy(gameObject);
    }

    void ReachEnd()
    {
        isAttacking = false;
        targetPlant = null;

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1);
        }

        if (spawner != null)
        {
            spawner.ZombieDied();
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlantHealth plant = other.GetComponent<PlantHealth>();

        if (plant != null)
        {
            isAttacking = true;
            targetPlant = plant;
        }

        if(other.CompareTag("Base"))
        {
            ReachEnd();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlantHealth>() != null)
        {
            isAttacking = false;
            targetPlant = null;
        }
    }

    public void Slow()
    {
        CancelInvoke("Unslowed");

        if (!isSlowed)
        {
            speed = originalSpeed * 0.5f;
            isSlowed = true;
            UpdateColor();
        }
        Invoke("Unslowed", 2.5f);
    }

    public void Freeze()
    {
        CancelInvoke("Unfreezed");

        if(!isFreezed)
        {
            speed = originalSpeed * 0f;
            isFreezed = true;
            iceCube.SetActive(true);
        }
        Invoke("Unfreezed", 2.5f);
    }

    void Unslowed()
    {
        isSlowed = false;

        if(isFreezed)
        {
            speed = 0f;
        }
        else
        {
        speed = originalSpeed;
        UpdateColor();
        
        }
    }

    void Unfreezed()
    {
        isFreezed = false;
        iceCube.SetActive(false);
        
        if (isSlowed)
        {
            speed = originalSpeed * 0.5f;
        }
        else
        {
            speed = originalSpeed;
            
        }
    }

    public void DifficultyStat()
    {
        speed *= GameSetting.difficulty;
        maxHealth = (int)(maxHealth * GameSetting.difficulty);
        damage = (int)(damage * GameSetting.difficulty);
        armorHealth = (int)(armorHealth * GameSetting.difficulty);
    }

    public void Fire()
    {
        CancelInvoke("UnFire");
        isFired = true;
        UpdateColor();
        Invoke("UnFire", 3f);
    }

    void UnFire()
    {
        isFired = false;
        UpdateColor();
    }

    void UpdateColor()
{
     if (isSlowed)
    {
        sr.color = Color.blue;
    }
    else if (isFired)
    {
        sr.color = Color.red;
    }
    else
    {
        sr.color = Color.white;
    }
}
}