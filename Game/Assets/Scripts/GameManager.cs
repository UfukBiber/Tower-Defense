using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Image PauseMenu;

    public GameObject gunToBuild;
    private float health = 100f;
    public GameObject[] Enemies;
    public GameObject EnemySpawner;
    public float gunPrice;
    private float totalMoney = 99999f;
    public GameObject placeToBuild;

    public Image gameOverImage;
    public TMP_Text totalMoneyText;
    public TMP_Text healthText;

    private bool isPaused = false;

    private void Awake()
    {
        gameOverImage.gameObject.SetActive(false);
        EnemySpawner = GameObject.Find("EnemySpawner");
        totalMoneyText.text = totalMoney.ToString() + "$";
        healthText.text = health.ToString();
    }

    private void Update()
    {
        UpdateEnemies();
        CheckGameOver();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        
    }

    private void CheckGameOver()
    {
        if (health <= 0)
        {
            EnemySpawner.SetActive(false);
            DestroyAllEnemies();
            gameOverImage.gameObject.SetActive(true);
        }
    }
    public void buildTheSelectedWeapon()
    {
        if (gunToBuild != null && totalMoney >= gunPrice)
        {
            totalMoney -= gunPrice;
            totalMoneyText.text = totalMoney.ToString() + "$";
            Instantiate(gunToBuild, placeToBuild.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            health -= 10;
            healthText.text = health.ToString();
            Destroy(other.gameObject);
        }
    }

    private void DestroyAllEnemies()
    {
        for (int i = 0; i < Enemies.Length; i++)
        {
            Destroy(Enemies[i]);
        }
    }

    public void upgradeWeapon(GameObject weaponToUpgrade)
    {
        GunControl gunControl = weaponToUpgrade.GetComponent<GunControl>();
        gunControl.range += 10f;
    }

    public void sellWeapon(GameObject weaponToSell)
    {
        if (weaponToSell.CompareTag("MachineGun"))
        {
            totalMoney += 50f;
            Destroy(weaponToSell);
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateEnemies()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void Pause()
    {
        UpdateEnemies();
        if (!isPaused)
        {
            for (int i = 0; i < Enemies.Length; i++) { Enemies[i].GetComponent<Enemy>().isPaused = true; }
            PauseMenu.gameObject.SetActive(true);
            isPaused = true;
            EnemySpawner.GetComponent<EnemySpawner>().isPaused = true;
        }
        else
        {
            for (int i = 0; i < Enemies.Length; i++) { Enemies[i].GetComponent<Enemy>().isPaused = false; }
            PauseMenu.gameObject.SetActive(false);
            isPaused = false;
            EnemySpawner.GetComponent<EnemySpawner>().isPaused = false;
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
