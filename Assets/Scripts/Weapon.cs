using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public WeaponData weaponData;
    public Transform armTarget;         
    public Transform firePoint;         
    public GameObject bulletPrefab;      

    public GameObject Avatar;

    private float fireRate; 
    private SpriteRenderer spriteRenderer;
    public float firstBulletTime=0.1f;
    private float lastFireTime = 0f;
    private bool isShooting = false;

    public GameObject shootparticle;
    public AudioManager audioManager;
    Bullet bulletScript;




private void Awake(){
    spriteRenderer = GetComponent<SpriteRenderer>();
}
private void Start()
    {
        ApplyWeaponData();  // Silah verilerini uygulama
    }

public void ApplyWeaponData()
    {
        fireRate = weaponData.fireRate;
        spriteRenderer.sprite = weaponData.sprite;
    }

void Update()
    {
        
        
        GameObject nearestEnemy = FindClosestVisibleEnemy();
        if (nearestEnemy != null && weaponData.weaponName!="noweapon")
        {
            Vector3 targetPosition = nearestEnemy.transform.position;
            armTarget.position = targetPosition; 
            if (armTarget.localPosition.x<0)
            {
                gameObject.transform.localScale=new Vector3(0.6f, -0.6f, 1);
            }
            else
            {
                gameObject.transform.localScale=new Vector3(0.6f, 0.6f, 1);
            }
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

   

    public void StartShooting()
    {
        if (Time.time >= lastFireTime + fireRate && !isShooting && weaponData.weaponName!="noweapon")
        {
            isShooting = true;
            InvokeRepeating("Shoot", firstBulletTime, fireRate);
        }
    }




    public void StopShooting()
    {
        isShooting = false;
        CancelInvoke("Shoot");
    }

    private void Shoot()
    {
        GameObject nearestEnemy = FindClosestVisibleEnemy();
        if (nearestEnemy!=null)
        {
             GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bulletParticle=Instantiate(shootparticle,firePoint.position,firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDamage(weaponData.damage); 
        StartCoroutine(destroyParticle(bulletParticle));
        Debug.Log("Ateş edildi");
        audioManager.PlaySFX(weaponData.shootSound); // Mermi sesini çal
        }
       
        
    }

    IEnumerator destroyParticle(GameObject bulletParticle){
        yield return new WaitForSeconds(1);
        Destroy(bulletParticle);
    }
    bool IsVisibleToCamera(GameObject obj)
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(obj.transform.position);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0;
    }
}
