using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class WaveManager : MonoBehaviour
{
    public int waveNumber = 1; 
    public float waveDuration = 30f; 
    public float enemySpeedMultiplier = 1f; 
    public float enemyDamageMultiplier = 1f; 
    public float enemyHealthMultiplier = 1f;
    public float spawnDelay=5f;
    public static WaveManager Instance { get; private set; }
    public delegate void OnWaveStart(int waveNumber, float speedMultiplier, float damageMultiplier,float healthMultipler,float spawnDelay);
    public static event OnWaveStart WaveStarted;

    public delegate void OnWaveEnd(int waveNumber);
    public static event OnWaveEnd WaveEnded;
    public GameObject inventoryUI;
    private float waveTimer;
    private bool isWaveActive = false;

    public TextMeshProUGUI wavetext;
    public TextMeshProUGUI wavetimertext;
    public WeaponManager weaponManager;
    public PlayerController player;
    public Inventory inventory;
    void Start()
    {
        inventory.AddHandWeapon(weaponManager.weaponDatas[0]);
        inventory.AddHandWeapon(weaponManager.weaponDatas[0]);
        StartNextWave();
    }


    void Update()
    {
        if (isWaveActive)
        {
            waveTimer -= Time.deltaTime;
            wavetimertext.text=waveTimer.ToString("F1");
            if (waveTimer <= 0)
            {
                EndWave();
            }
        }
    }
     private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartNextWave()
    {
        UpdateWeaponData();
        isWaveActive = true;
        waveTimer = waveDuration;
        inventoryUI.SetActive(false);
        wavetext.text="Wave "+waveNumber.ToString();
        
        enemySpeedMultiplier = 1f + (waveNumber - 1) * 0.1f; 
        enemyHealthMultiplier = 1f + (waveNumber - 1) * 0.1f; 
        enemyDamageMultiplier = 1f + (waveNumber - 1) * 0.2f; 
        waveDuration+=waveDuration*0.1f;
        spawnDelay =spawnDelay-spawnDelay*0.1f;
    
        
        
        WaveStarted?.Invoke(waveNumber, enemySpeedMultiplier, enemyDamageMultiplier,enemyHealthMultiplier,spawnDelay);
        
        Debug.Log($"Wave {waveNumber} başladı! Süre: {waveDuration}sn, Hız Çarpanı: {enemySpeedMultiplier},Can Çarpanı {enemyHealthMultiplier}, Hasar Çarpanı: {enemyDamageMultiplier}");
    }

    public void EndWave()
    {
        isWaveActive = false;
        

        
        
        
        
        DestroyAllEnemies();
  
        WaveEnded?.Invoke(waveNumber);

        Debug.Log($"Wave {waveNumber} bitti! Kart seçimi başlıyor.");

       player.ResetGrenade();
        player.ResetPlayer();
        waveNumber++;
    }


    private void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); 
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy); 
        }
    }

    private void UpdateWeaponData(){
            
                weaponManager.ChangeWeapon(0,inventory.Handweapons[0]);
                weaponManager.ChangeWeapon(1,inventory.Handweapons[1]);
    }
} 
