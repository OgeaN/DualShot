using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public WeaponData weapon;
    private Transform originalParent;
    private Image image;
    
    void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root);  // Üst seviyeye taşı
        transform.SetAsLastSibling();  // En öne getir
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        image.raycastTarget = true;
        ResetToParentCenter();
    }

    public void OnDrop(PointerEventData eventData)
    {
        DragAndDrop other = eventData.pointerDrag.GetComponent<DragAndDrop>();
        Debug.Log(other.weapon.weaponName);
        Debug.Log("bekleyen "+this.transform.parent +"sürüklediğimiz "+ other.transform.parent);
        if (other!=null && other.weapon != weapon)
        {
            Inventory inventory = FindObjectOfType<Inventory>();
            inventory.SwapWeapons(weapon, other.weapon,this.transform.parent,other.originalParent);
        }
        else if (other != null && other.weapon == weapon && weapon.nextLevelWeapon != null)
        {
            Inventory inventory = FindObjectOfType<Inventory>();
            Debug.Log("bekleyen "+this.transform.parent +"sürüklediğimiz "+ other.transform.parent + "orjinal "+other.originalParent);
            inventory.MergeWeapons(weapon, other.weapon,this.transform.parent,other.originalParent);
        }
        else
        {
            Debug.Log("bekleyen "+this.transform.parent +"sürüklediğimiz "+ other.transform.parent);
            ResetToParentCenter();
        }
    }


    private void ResetToParentCenter()
    {
        if (transform.parent != null)
        {
            transform.localPosition = Vector3.zero;
        }
    }
}
