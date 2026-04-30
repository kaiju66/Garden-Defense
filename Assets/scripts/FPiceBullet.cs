using UnityEngine;

public class FPiceBullet : MonoBehaviour
{
    public float speed = 2f;
    public int damage = 10;

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
            z.Freeze();

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