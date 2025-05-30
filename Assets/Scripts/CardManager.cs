// Scripts/CardManager.cs
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<CardSO> allCards;          // Kart veri listesi
    public GameObject cardPrefab;          // Kart UI prefabı
    public Transform cardSpawnParent;      // Kartların gösterileceği panel
    public GameObject inventoryUI;

    public WaveEndScreenManager waveEndScreenManager; // WaveEndScreenManager referansı
    public void ShowCards()
    {
        ClearCards();
        List<int> randomIndexes = new List<int>();

        while (randomIndexes.Count < 3 && randomIndexes.Count < allCards.Count)
        {
            int rand = Random.Range(0, allCards.Count);
            if (!randomIndexes.Contains(rand))
            {
                randomIndexes.Add(rand);
            }
        }
        foreach (int index in randomIndexes)
        {
            CardSO selectedCard = allCards[index];
            GameObject cardGO = Instantiate(cardPrefab, cardSpawnParent);
            CardUI ui = cardGO.GetComponent<CardUI>();
            ui.Setup(selectedCard, OnCardSelected);
        }
    }

    void ClearCards()
    {
        foreach (Transform child in cardSpawnParent)
        {
            Destroy(child.gameObject);
        }
    }

    void OnCardSelected(CardSO selectedCard)
    {
        // Etkileri uygula
        waveEndScreenManager.ApplyCardEffects(selectedCard);
        // Paneli kapat
        gameObject.SetActive(false);
        inventoryUI.SetActive(true); 
    }
}
