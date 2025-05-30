using UnityEngine;
using TMPro; // TextMeshPro desteği için
using System.Collections.Generic;
public class WaveEndScreenManager : MonoBehaviour
{
    public GameObject waveEndScreen; // Wave sonu ekranı (panel veya canvas)
    public TextMeshProUGUI waveEndText; // Wave numarasını göstermek için text
    public WaveManager waveManager; // WaveManager referansı
    public PlayerController playerController; // PlayerController referansı
    public Enemy enemyController; // EnemyController referansı

    public WeaponManager weaponManager;
    

    void OnEnable()
    {
        WaveManager.WaveEnded += ShowWaveEndScreen;
    }

    void OnDisable()
    {
        WaveManager.WaveEnded -= ShowWaveEndScreen;
    }

    private void Start()
    {
        waveEndScreen.SetActive(false); // Ekran başlangıçta gizli
    }

    private void ShowWaveEndScreen(int waveNumber)
    {
        waveEndText.text = $"Wave {waveNumber} Completed!"; // Ekrandaki metni güncelle
        waveEndScreen.SetActive(true); // Wave bitiş ekranını göster
        waveEndScreen.GetComponent<CardManager>().ShowCards();
    }

    
    
    public void ApplyCardEffects(CardSO card)
    {
        GameModifiers.Instance.enemyDropChanceMultiplier*=1 + (card.extraWeaponDropChance / 100f);
        GameModifiers.Instance.enemyHealthMultiplier*=1 + (card.extraEnemyHealth / 100f);
        GameModifiers.Instance.enemySpeedMultiplier*=1 + (card.extraEnemySpeed / 100f);
        GameModifiers.Instance.enemyDamageMultiplier*=1 + (card.extraEnemyDamage / 100f);
        waveManager.spawnDelay -= waveManager.spawnDelay * card.extraEnemySpawnRate/ 100f;
        playerController.currentHealth += (int)(playerController.currentHealth * card.extraPlayerHealth / 100f);
        playerController.speed += playerController.speed * card.extraPlayerSpeed / 100f;
    }
        
    }
     

    /*
    daha fazla silah düşme şansı
    daha fazla düşman spawn etme şansı
    daha fazla düşman spawn etme hızı
    daha fazla düşman hasarı
    daha fazla düşman hızı
    daha fazla düşman canı
    daha fazla karakter canı
    daha fazla karakter hızı
    */

