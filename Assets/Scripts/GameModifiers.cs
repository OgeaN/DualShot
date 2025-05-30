using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModifiers : MonoBehaviour
{
    public static GameModifiers Instance;

    public float enemyHealthMultiplier = 1f;
    public float enemyDamageMultiplier = 1f;
    public float enemySpeedMultiplier = 1f;

    public float enemyDropChanceMultiplier =1f; // 10% chance to drop a weapon

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ResetAll() 
    {
        enemyHealthMultiplier = 1f;
        enemyDamageMultiplier = 1f;
        enemySpeedMultiplier = 1f;
        enemyDropChanceMultiplier = 1f; // Reset drop chance multiplier
    }
}

