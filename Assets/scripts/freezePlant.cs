using UnityEngine;

public class FreezePlant : MonoBehaviour
{
    public GameObject FPnormalBullet;
    public GameObject FPfreezeBullet;
    public GameObject FPiceBullet;
    public Transform shootPoint;

    public float shootDelay = 3f;
    public float range = 8f;
    public LayerMask zombieLayer;

    private float timer;

    void Awake()
    {
        if (shootPoint == null)
        {
            Debug.LogError("shootPoint  не призначино");
        }

        if (FPnormalBullet == null)
        {
            Debug.LogError("FPnormalBullet не призначено");
        }

        if (FPfreezeBullet == null)
        {
            Debug.LogError("FPfreezeBullet не призначено");
        }

        if (FPiceBullet == null)
        {
            Debug.LogError("FPiceBullet не призначено");
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, range, zombieLayer);
        Debug.DrawRay(transform.position, Vector2.right * range, Color.red);
        return hit.collider != null;
    }

    void Shoot()
    {
        if (FPnormalBullet == null || FPfreezeBullet == null || FPiceBullet == null || shootPoint == null) return;

       int bulletType = Random.Range(1, 4);

       if (bulletType == 1)
       {
        Instantiate(FPnormalBullet, shootPoint.position, Quaternion.identity);
       }

       if (bulletType == 2)
       {
        Instantiate(FPfreezeBullet, shootPoint.position, Quaternion.identity);
       }

       if (bulletType == 3)
       {
        Instantiate(FPiceBullet, shootPoint.position, Quaternion.identity);
       }
    }
}