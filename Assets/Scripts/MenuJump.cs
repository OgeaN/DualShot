using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuJump : MonoBehaviour
{
    public void MainMenu(){
        SceneManager.LoadScene(0);
    }

    public void GameMenu(){
        SceneManager.LoadScene(1);
    }

}
