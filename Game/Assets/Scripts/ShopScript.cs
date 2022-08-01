using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject MachineGun;
    public GameObject RocketLauncher;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void BuyMachineGun()
    {
        gameManager.gunToBuild = MachineGun;
        gameManager.gunPrice = 150f;
    }

    public void BuyRocketLauncher()
    {
        gameManager.gunToBuild = RocketLauncher;
        gameManager.gunPrice = 250f;
    }
}
