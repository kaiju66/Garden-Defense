using UnityEngine;

public class fireBullet : MonoBehaviour
{
    public int damage = 30;
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Zombie"))
        {
            Zombie z = other.GetComponent<Zombie>();

            z.TakeDamage(damage);
            z.Fire();

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