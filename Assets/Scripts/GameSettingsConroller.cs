using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsConroller : MonoBehaviour
{
    [SerializeField]private GameObject settingsMenu;
    public Button myButton;

    void Start()
    {
        // Butonun onClick olayına bir metod ekleyin
        myButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else if (!settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
