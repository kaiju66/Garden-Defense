using UnityEngine;

public class FPfreezeBullet : MonoBehaviour
{
    public float speed = 3f;
    public int damage = 15;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Zombie"))
        {
            Zombie z = other.GetComponent<Zombie>();

            z.TakeDamage(damage);
            z.Slow();

            Destroy(gameObject);
        }
    }

    void Start()
    {
        Invoke("DestroyBullet", 5f);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}