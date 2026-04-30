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

    void Start()
    {
        currentHealth = maxHealth;

        playerHealth = FindAnyObjectByType<PlayerHealth>();

        Invoke(nameof(EnableMovement), startMoveDelay);

        originalSpeed = speed;
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

        // дійшов до кінця
        if (transform.position.x < destroyX)
        {
            ReachEnd();
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

            armorHealth -= dmg;

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
        }
        Invoke("Unslowed", 1.5f);
    }

    public void Freeze()
    {
        CancelInvoke("Unfreezed");

        if(!isFreezed)
        {
            speed = originalSpeed * 0f;
            isFreezed = true;
        }
        Invoke("Unfreezed", 1.5f);
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
        
        }
    }

    void Unfreezed()
    {
        isFreezed = false;
        
        if (isSlowed)
        {
            speed = originalSpeed * 0.5f;
        }
        else
        {
            speed = originalSpeed;
            
        }
    }
}