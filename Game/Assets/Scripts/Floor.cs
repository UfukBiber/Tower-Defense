using UnityEngine;

public class Floor : MonoBehaviour
{
    public Material highlightColor;
    public Material occupiedColor;
    public Material originalColor;



    private GameObject currentWeapon;
    
    public bool isOccupied;

    public float machineGunPrice;
    public float rocketLauncherPrice;

   
    public MeshRenderer meshRenderer;
    void Start()
    {
        isOccupied = false;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = originalColor;
    }

    private void OnMouseEnter()
    {
        if (isOccupied) { return; }
        else
        {
            if (GameManager.instance.machineGunSelected)
            {
                currentWeapon = Instantiate(GameManager.instance.machineGun, transform.position, Quaternion.identity);
                meshRenderer.material = highlightColor;
            }
            else if (GameManager.instance.rocketLauncherSelected)
            {
                currentWeapon = Instantiate(GameManager.instance.rocketLauncher, transform.position, Quaternion.identity);
                meshRenderer.material = highlightColor;
            }
        }
    }

    private void OnMouseDown()
    {
        if (!isOccupied)
        {
            if (GameManager.instance.machineGunSelected)
            {
                isOccupied = true;
                meshRenderer.material = occupiedColor;
                GameManager.instance.money -= machineGunPrice;
                GameManager.instance.machineGunSelected = false;
            }
            else if (GameManager.instance.rocketLauncherSelected)
            {
                isOccupied = true;
                meshRenderer.material = occupiedColor;
                GameManager.instance.money -= rocketLauncherPrice;
                GameManager.instance.rocketLauncherSelected = false;
            }
        }
        else
        {
            GameManager.instance.weaponMenu.SetActive(true);
            GameManager.instance.selectedFloor = this;
        }
    }
    private void OnMouseExit()
    {
        if (!isOccupied) { Destroy(currentWeapon); meshRenderer.material = originalColor; }
    }

    public void UpdateWeapon()
    {
        if (currentWeapon.CompareTag("MachineGun"))
        {
            currentWeapon.GetComponent<MachineGun>().Upgrade();
           
        }
        else
        {
            currentWeapon.GetComponent<RocketLauncher>().Upgrade();
        }
    }
    public void SellWeapon()
    {
        isOccupied = false;
        meshRenderer.material = originalColor;
        Destroy(currentWeapon);
        currentWeapon = null;
        GameManager.instance.money += 10f;
    }
}
