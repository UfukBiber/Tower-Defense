using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public Color highlightedColor;
    private GameManager gameManager;
    public GameObject machineGunRange;
    public GameObject rocketLauncherRange;
    private GameObject currentRange;
    public MeshRenderer render;
    
    void Awake()
    {
        render = GetComponent<MeshRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    private void OnMouseEnter()
    {
        if (gameManager.gunToBuild != null)
        {
            if (gameManager.gunToBuild.CompareTag("MachineGun"))
            {
                currentRange = Instantiate(machineGunRange, transform.position, Quaternion.identity);
            }
            else if (gameManager.gunToBuild.CompareTag("RocketLauncher"))
            {
                currentRange = Instantiate(rocketLauncherRange, transform.position, Quaternion.identity);   
            }
        }
    }

    private void OnMouseExit()
    {
        render.material.color = Color.white;
        Destroy(currentRange);
    }
    private void OnMouseDown()
    {
        gameManager.placeToBuild = gameObject;
        gameManager.buildTheSelectedWeapon();
        Destroy(currentRange);
        gameManager.gunToBuild = null;
        gameManager.placeToBuild = null;
    }
}
