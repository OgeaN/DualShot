using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage=40;
    public float speed = 20f;
    public Rigidbody2D rb;
    private Transform target;
    GameObject enemy;
    private bool hasDamaged = false;

    void Start()
    {
        enemy = FindClosestVisibleEnemy();
        if (enemy != null)
        {
            target = enemy.transform;
        }
    }

    void Update()
    {
        if (target != null && enemy.tag=="Enemy")
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
        else
        {
            Destroy(gameObject);
            rb.velocity = transform.right * speed;
        }
    }

    GameObject FindClosestVisibleEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            if (IsVisibleToCamera(enemy))
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }

    bool IsVisibleToCamera(GameObject obj)
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(obj.transform.position);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0;
    }

    void OnTriggerEnter2D(Collider2D other) {
        
        if (!hasDamaged)
        {
            if (other.CompareTag("Enemy"))
        {
            hasDamaged = true;
            Debug.Log(other.name);
            Enemy enemy=other.GetComponent<Enemy>();

            if (enemy!=null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
            
        }
        }
    }
        
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}
