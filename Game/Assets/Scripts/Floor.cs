using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public Color highlightColor;
    public Color occupiedColor;
    public Color originalColor;
    public GameObject Shop;
    public GameObject selfShop;
    public GameObject currentWeapon;
    public bool isSelected;
    

    public bool isOccupied;

    public MeshRenderer meshRenderer;
    void Start()
    {
        isSelected = false;
        isOccupied = false;
        meshRenderer = GetComponent<MeshRenderer>();
        highlightColor = Color.white;
        originalColor = Color.green;
        occupiedColor = Color.red;
        meshRenderer.material.color = originalColor;
    }

    private void OnMouseEnter()
    {
        if (!isSelected || !isOccupied)
        {
            meshRenderer.material.color = highlightColor;
        }
    }
    private void OnMouseExit()
    {
        if (!isSelected || !isOccupied)
        {
            meshRenderer.material.color = originalColor;
        }
        if (selfShop != null)
        {
            selfShop.GetComponent<Shop>().opened = true;
        }
    }

    private void OnMouseDown()
    {
        if (selfShop != null) { Destroy(selfShop); isSelected = false; }
        else
        {
            selfShop = Instantiate(Shop, transform.position, Quaternion.identity);
            selfShop.GetComponent<Shop>().parentFloor = transform;
            isSelected = true;
        }
        
    }

    public void Occupy()
    { 
        isOccupied = true;
        meshRenderer.material.color = occupiedColor;
    }

    public void SellWeapon()
    {
        isOccupied = false;
        meshRenderer.material.color = originalColor;
        Destroy(currentWeapon);
    }
}
