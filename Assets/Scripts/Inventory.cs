using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public List<WeaponData> weapons = new List<WeaponData>();

    public List<WeaponData> Handweapons = new List<WeaponData>();
    public Transform inventoryPanel;

    public Transform handPanel;
    public GameObject weaponSlotPrefab;

    public void AddWeapon(WeaponData newWeapon)
    {
        weapons.Add(newWeapon);
        UpdateUI();
    }

    public void AddHandWeapon(WeaponData newWeapon)
    {
        Handweapons.Add(newWeapon);
        UpdateUI();
    }

    public void MergeWeapons(WeaponData bekleyenWeapon1, WeaponData suruklenenWeapon2,Transform bekleyenParent,Transform suruklenenParent)
{
    if (bekleyenWeapon1 == suruklenenWeapon2 && bekleyenWeapon1.nextLevelWeapon != null)
    {
        // Silahların hangi listede olduğunu bul
        if (bekleyenParent.name == "Weapon1" || bekleyenParent.name == "Weapon2")
        {
            Handweapons.Remove(bekleyenWeapon1);
            Handweapons.Add(bekleyenWeapon1.nextLevelWeapon);
            
        }
        else
        {
            weapons.Remove(bekleyenWeapon1);
            weapons.Add(bekleyenWeapon1.nextLevelWeapon);
        }
        if (suruklenenParent.name == "Weapon1" || suruklenenParent.name == "Weapon2")
        {
            Handweapons.Remove(suruklenenWeapon2);
        }
        else
        {
            weapons.Remove(suruklenenWeapon2);
        }
        if (suruklenenParent.name == "Weapon1" || suruklenenParent.name == "Weapon2" &&bekleyenParent.name == "Weapon1" || bekleyenParent.name == "Weapon2")
        {
            StartCoroutine(WaitAndUpdateUI());
        }
        // UI Güncelle
        UpdateUI();
    }
}

IEnumerator WaitAndUpdateUI()
{
    yield return new WaitForSeconds(0.1f);
    UpdateUI();
}
public void SwapWeapons(WeaponData weapon1, WeaponData weapon2,Transform bekleyenParent,Transform suruklenenParent)
{
    if (bekleyenParent.name=="Weapon1" || bekleyenParent.name== "Weapon2")
    {
        if (suruklenenParent.name=="Weapon1" || suruklenenParent.name== "Weapon2")
        {
            return;
        }else if (weapon1==null)
        {
            weapons.Remove(weapon2);
            Handweapons.Remove(weapon1);
            Handweapons.Add(weapon2);
        }
        else
        {
            weapons.Remove(weapon2);
            Handweapons.Remove(weapon1);
            weapons.Add(weapon1);
            Handweapons.Add(weapon2);
        }
    }
    if (suruklenenParent.name=="Weapon1" || suruklenenParent.name== "Weapon2")
    {
        if (bekleyenParent.name=="Weapon1" || bekleyenParent.name== "Weapon2")
        {
            return;
        }
        else if (weapon2==null)
        {
            Handweapons.Remove(weapon2);
            weapons.Remove(weapon1);
            Handweapons.Add(weapon1);
        }
        else
        {
            Handweapons.Remove(weapon2);
            weapons.Remove(weapon1);
            Handweapons.Add(weapon1);
            weapons.Add(weapon2);
        }
    }
    UpdateUI();
}
void UpdateUI()
{
    // 1️⃣ Envanter panelini temizle
    foreach (Transform child in inventoryPanel)
    {
        Destroy(child.gameObject);
    }

    // 2️⃣ Yeni silah slotlarını oluştur
    foreach (var weapon in weapons)
    {
        GameObject slot = Instantiate(weaponSlotPrefab, inventoryPanel);
        Image image = slot.transform.Find("Icon").GetComponent<Image>();
        image.GetComponent<DragAndDrop>().weapon = weapon;
        image.sprite = weapon.sprite;
    }

    // 3️⃣ "Weapons" GameObject'ini **TAG kullanarak** bul
    if (handPanel == null)
    {
        Debug.LogError("HandPanel referansı atanmadı!");
        return;
    }
    

    GameObject weapon1 = handPanel.transform.Find("Weapon1")?.gameObject;
    GameObject weapon2 = handPanel.transform.Find("Weapon2")?.gameObject;

    if (weapon1 == null || weapon2 == null)
    {
        Debug.LogError("Weapon1 veya Weapon2 bulunamadı!");
        return;
    }

    Image iconImage1 = weapon1.transform.Find("Icon")?.GetComponent<Image>();
    Image iconImage2 = weapon2.transform.Find("Icon")?.GetComponent<Image>();

    if (iconImage1 == null || iconImage2 == null)
    {
        Debug.LogError("Icon nesnelerinde Image bileşeni bulunamadı!");
        return;
    }

    // 4️⃣ Handweapons listesini kontrol et
    if (Handweapons.Count >= 2)
{
    // 2 silah varsa her iki ikonu da doldur
    iconImage1.sprite = Handweapons[0].sprite;
    iconImage2.sprite = Handweapons[1].sprite;
    iconImage1.GetComponent<DragAndDrop>().weapon = Handweapons[0];
    iconImage2.GetComponent<DragAndDrop>().weapon = Handweapons[1];
    iconImage1.color = new Color(1, 1, 1, 1); // Görünür yap
    iconImage2.color = new Color(1, 1, 1, 1); // Görünür yap
}
else if (Handweapons.Count == 1)
{
    // 1 silah varsa sadece ilk ikonu doldur
    iconImage1.sprite = Handweapons[0].sprite;
    iconImage1.GetComponent<DragAndDrop>().weapon = Handweapons[0];
    iconImage2.sprite = null; // İkinci ikon boş kalsın
    iconImage2.GetComponent<DragAndDrop>().weapon = null;
    iconImage1.color = new Color(1, 1, 1, 1); // Görünür yap
    iconImage2.color = new Color(1, 1, 1, 0); // Görünmez yap
}
else
{
    // Silah yoksa her iki ikonu da boş yap
    iconImage1.sprite = null;
    iconImage2.sprite = null;
    iconImage1.GetComponent<DragAndDrop>().weapon = null;
    iconImage2.GetComponent<DragAndDrop>().weapon = null;
    iconImage1.color = new Color(1, 1, 1, 0); // Görünmez yap
    iconImage2.color = new Color(1, 1, 1, 0); // Görünmez yap
}


    Debug.Log("UI başarıyla güncellendi!");
}

}
