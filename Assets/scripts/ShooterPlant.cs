using UnityEngine;

public class ShooterPlant : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;

    public float shootDelay = 1.5f;
    public float range = 6f;
    public LayerMask zombieLayer; // Тільки об'єкти з цим шаром будуть перевірятися

    private float timer;

    void Awake()
    {
        // Автоматичне знаходження ShootPoint, якщо не призначено вручну
        if (shootPoint == null)
        {
            shootPoint = transform.Find("ShootPoint");
            if (shootPoint == null)
            {
                // Якщо немає, створюємо його автоматично
                GameObject sp = new GameObject("ShootPoint");
                sp.transform.parent = transform;
                sp.transform.localPosition = Vector3.zero; // можна підкоригувати
                shootPoint = sp.transform;
            }
        }

        if (bulletPrefab == null)
        {
            Debug.LogWarning("ShooterPlant: Bullet Prefab не призначено!");
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (IsZombieAhead())
        {
            if (timer >= shootDelay)
            {
                Shoot();
                timer = 0f;
            }
        }
    }

    bool IsZombieAhead()
    {
        // Пробиваємо рейкаст лише по шарах zombieLayer
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, range, zombieLayer);
        Debug.DrawRay(transform.position, Vector2.right * range, Color.red); // для зручності в редакторі
        return hit.collider != null;
    }

    void Shoot()
    {
        if (bulletPrefab == null || shootPoint == null) return;

        Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
    }
}