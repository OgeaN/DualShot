using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float fireRate;
    public Sprite sprite;
    public int damage;

    public Color baseColor;
    public AudioClip shootSound;
    public WeaponData nextLevelWeapon;
}
