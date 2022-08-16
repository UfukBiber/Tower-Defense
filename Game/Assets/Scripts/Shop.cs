using UnityEngine;

public class Shop : MonoBehaviour
{
    public Canvas canvas;
    public Transform parentFloor;
    private Floor floor;
    public GameObject weaponMenuCanvas;
    public GameObject shopCanvas;
    public GameObject machineGun;
    public bool opened = false;
    private bool isMouseIn;
    private float machineGunPrice = 25f;
    public float machineGunUpgradePrice = 15f;
    public float weaponSalePrice = 10f;
   

    
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        floor = parentFloor.GetComponent<Floor>();
        if (floor.isOccupied) { weaponMenuCanvas.SetActive(true); transform.position = transform.position + new Vector3(2.5f, 5f, 0f); }
        else { shopCanvas.SetActive(true); transform.position = transform.position + new Vector3(1.5f, 3f, 0f); }
        transform.rotation = Quaternion.Euler(60f, 90f, 0f);
        
    }

    private void Update()
    {
        if (opened && Input.GetMouseButtonDown(0) && !isMouseIn)
        {
            Destroy(gameObject);
            floor.isSelected = false;
        }
    }
    private void OnMouseEnter()
    {
        isMouseIn = true;
    }
    private void OnMouseExit()
    {
        isMouseIn = false;
    }

    public void BuildMachineGun()
    {
        if (GameManager.instance.money >= machineGunPrice)
        {
            GameManager.instance.money -= machineGunPrice;
            floor.currentWeapon = Instantiate(machineGun, parentFloor.position, Quaternion.identity);
            floor.Occupy();
            Destroy(gameObject);
        }
    }
    public void UpgradeWeapon()
    {
        if (GameManager.instance.money >= machineGunUpgradePrice)
        {
            Debug.Log("UpgradeWeapon");
            GameManager.instance.money -= machineGunUpgradePrice;
            floor.currentWeapon.GetComponent<MachineGun>().Upgrade();
            Destroy(gameObject);
        }
    }

    public void SellWeapon()
    {
        Debug.Log("SellWeapon");
        GameManager.instance.money += weaponSalePrice;
        floor.SellWeapon();
        Destroy(gameObject);
    }    
}
