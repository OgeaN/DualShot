using UnityEngine;
using TMPro; // TextMeshPro desteği için

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
    }

    
    public void OnNextWaveButtonClicked()
    {
        
        waveEndScreen.SetActive(false); 
        waveManager.StartNextWave();
    }

    public void OnNextWaveButtonClicked2()
    {
       
        waveEndScreen.SetActive(false); 
        waveManager.StartNextWave();
    }

    public void OnNextWaveButtonClicked3()
    {
        
        waveEndScreen.SetActive(false); 
        waveManager.StartNextWave();
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
}
