using UnityEngine;

public class Floor : MonoBehaviour
{
    public Material highlightColor;
    public Material occupiedColor;
    public Material originalColor;

    public GameObject rangeView;
    private GameObject currentRangeView;

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
                currentRangeView = Instantiate(rangeView, transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
                currentRangeView.GetComponent<Range>().range = currentWeapon.GetComponent<MachineGun>().range;
                meshRenderer.material = highlightColor;
            }
            else if (GameManager.instance.rocketLauncherSelected)
            {
                currentWeapon = Instantiate(GameManager.instance.rocketLauncher, transform.position, Quaternion.identity);
                currentRangeView = Instantiate(rangeView, transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
                currentRangeView.GetComponent<Range>().range = currentWeapon.GetComponent<RocketLauncher>().range;
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
            Destroy(currentRangeView);
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
        Destroy(currentRangeView);
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
