using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponData [] weaponDatas;
    [SerializeField] GameObject [] handData;
    private Weapon handScript;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeWeapon(int handIndex,WeaponData weapon){
        Debug.Log("silah değişti");
        GameObject hand=handData[handIndex];
        handScript = hand.GetComponent<Weapon>();
        
        if (handScript != null)
        {
            handScript.weaponData=weapon;
            handScript.ApplyWeaponData();
        }
    }
}
