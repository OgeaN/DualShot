using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    private Transform target;
    public float speed = 3f;
    public float damageInterval = 1f;
    private float damageTimer = 0f;
    public int damage = 50;
    private bool isCollidingWithPlayer = false;
    private bool isDead = false;
    private bool isBorn = false;
    private Rigidbody2D rb;
    private GameObject collisenObject;
    private TextMeshProUGUI puanText;
    public Animator animator;
    public Slider slider;
    public GameObject healthBar;

    public List<WeaponData> possibleDrops;
    public Inventory playerInventory;
    public GameObject weaponPrefab;
    public float DropChance=15f;
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        slider.maxValue = health;
        UpdateSliderHealth(health);
        StartCoroutine(GecikmeliBaslat());
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        GameObject textObject = GameObject.FindGameObjectWithTag("Puan");
        puanText = textObject.GetComponent<TextMeshProUGUI>();

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
        WaveManager.WaveStarted += OnWaveStarted;
        ApplyInitialWaveModifiers();
        
    }

    IEnumerator GecikmeliBaslat()
    {
        yield return new WaitForSeconds(1.3f);
        isBorn = true;

    }
    void OnDestroy()
    {
        
        WaveManager.WaveStarted -= OnWaveStarted;
    }

    void OnWaveStarted(int waveNumber, float speedMultiplier, float damageMultiplier,float healthMultipler,float spawnDelay)
    {
        UpdateStats(speedMultiplier, damageMultiplier,healthMultipler);
        
    }

    public void UpdateStats(float speedMultiplier, float damageMultiplier,float healthMultipler)
    {   
        if (animator != null)
        {
             animator.speed =animator.speed* speedMultiplier;
        }
        speed *= speedMultiplier;
        health*=healthMultipler;
        damageInterval /= speedMultiplier;
        damage = Mathf.RoundToInt(damage * damageMultiplier);
        Debug.Log($"Enemy güncellendi: Hız = {speed}, Hasar = {damage}, Sağlık = {health}");
        slider.maxValue = health;
        UpdateSliderHealth(health); 
    }

    private void ApplyInitialWaveModifiers()
    {
        if (WaveManager.Instance != null)
        {
            float speedMultiplier = WaveManager.Instance.enemySpeedMultiplier;
            float damageMultiplier = WaveManager.Instance.enemyDamageMultiplier;
            float healthMultipler = WaveManager.Instance.enemyHealthMultiplier;
            UpdateStats(speedMultiplier, damageMultiplier,healthMultipler);
            UpdateStats(GameModifiers.Instance.enemySpeedMultiplier, GameModifiers.Instance.enemyDamageMultiplier, GameModifiers.Instance.enemyHealthMultiplier);
            DropChance*=GameModifiers.Instance.enemyDropChanceMultiplier; 
        }
    }

    void DropWeapon()
{
    if (possibleDrops.Count > 0 && weaponPrefab != null)
    {
        // Prefabı sahneye instantiate et
        if (Random.Range(0, 100) < DropChance) // DropChance yüzdelik bir değer olarak ayarlanır
        {
            WeaponData droppedWeapon = possibleDrops[Random.Range(0, possibleDrops.Count)];
            GameObject weaponObject = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
            weaponObject.name = droppedWeapon.weaponName;
            weaponObject.GetComponent<SpriteRenderer>().sprite = droppedWeapon.sprite;
            // Silahın ismini ve sprite'ını ayarla
        
        
            // WeaponPickup bileşeni varsa, içindeki veriyi güncelle
            WeaponPickup pickup = weaponObject.GetComponent<WeaponPickup>();
            if (pickup != null)
            {
                pickup.weapon = droppedWeapon;
            }
        }
        
        
    }
}

    void FixedUpdate()
    {
        if (!isCollidingWithPlayer && !isDead && isBorn)
        {
            MoveTowardsPlayer();
        }

        if (isCollidingWithPlayer)
        {
            damageTimer -= Time.deltaTime;

            if (damageTimer <= 0f)
            {
                AttackPlayer(collisenObject);
                damageTimer = damageInterval;
            }
        }

        Vector3 oyuncuPozisyonu = target.transform.position;
        Vector3 dusmanPozisyonu = transform.position;

        float mesafe = oyuncuPozisyonu.x - dusmanPozisyonu.x;

        if (mesafe > 0 && !isDead)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (mesafe < 0&& !isDead)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = target.position - transform.position;
        direction.y += 2;
        rb.velocity = direction.normalized * speed;
    }

    void AttackPlayer(GameObject player)
    {
        PlayerController playerHealth = player.GetComponent<PlayerController>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
        Debug.Log("Enemy is attacking the player!");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateSliderHealth(health);
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        DropWeapon();
        int puan = int.Parse(puanText.text);
        puan += 100;
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        puanText.text = puan.ToString();
        rb.velocity = Vector2.zero;
        isDead = true;
        gameObject.tag = "Untagged";
        animator.SetTrigger("DeadTrig");
        healthBar.SetActive(false);
        StartCoroutine(WaitAndPrint());
    }

    IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            animator.SetBool("isAttack", true);
            isCollidingWithPlayer = true;
            collisenObject = collider.gameObject;
            rb.velocity = Vector2.zero;
            damageTimer = damageInterval;
            Debug.Log("Düşman temasa başladı");
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            animator.SetBool("isAttack", false);
            Debug.Log("Düşman temas kesildi");
            isCollidingWithPlayer = false;
            rb.velocity = Vector2.zero;
        }
    }

    private void UpdateSliderHealth(float health)
    {
        slider.value = health;
    }
}
