// Scripts/CardUI.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CardUI : MonoBehaviour
{
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI descriptionText;
    public Image iconImage;
    private CardSO cardData;
    private UnityAction<CardSO> onSelected;

    public void Setup(CardSO card, UnityAction<CardSO> onSelectedCallback)
    {
        cardData = card;
        cardNameText.text = card.cardName;
        descriptionText.text = card.description;
        iconImage.sprite = card.icon;
        onSelected = onSelectedCallback;
    }

    public void OnClick()
    {
        onSelected?.Invoke(cardData);
    }
}
