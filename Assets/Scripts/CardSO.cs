using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class CardSO : ScriptableObject
{
    public string cardName;
    public string description;
    public Sprite icon;

    // Kartın vereceği etkiler için parametreler
    public float extraWeaponDropChance;   // Silah düşme şansı arttırma
    public float extraEnemySpawnRate;
    public float extraEnemyDamage;
    public float extraEnemySpeed;
    public float extraEnemyHealth;
    public float extraPlayerHealth;
    public float extraPlayerSpeed;
}
