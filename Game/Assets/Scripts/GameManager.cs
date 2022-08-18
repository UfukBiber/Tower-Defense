using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    public Transform[] enemyRoute1;
    public Transform[] enemyRoute2;
    public Transform[] enemyRoute3;
    private int route; // if 0 route1 is selected; if 1 route2 is selected;
    public GameObject[] shops;
    public GameObject[] enemies;
    public GameObject[] machineGuns;
    public GameObject enemy;

    public int maxEnemyNumberPerSpawn;
    public int minEnemyNumberPerSpawn;
    public float durationBetweenSpawns;
    public float frequencyInSpawn;
    private bool isDone;
    
    public float money;
    public float health;
    public TMP_Text moneyText;
    public TMP_Text healthText;
    public GameObject gameOverObject;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
        money = 100;
        health = 5f;
        minEnemyNumberPerSpawn = 1;
        maxEnemyNumberPerSpawn = 3;
        frequencyInSpawn = 1f;
        isDone = true;
    }

    void Update()
    {
        UpdateMenus();
        UpdateEnemies();
        UpdateWeapons();
        SendEnemies();
        PrintHealthAndMoney();
    }

    void UpdateMenus()
    {
        shops = GameObject.FindGameObjectsWithTag("Shop");
    }

    void UpdateEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void UpdateWeapons()
    {
        machineGuns = GameObject.FindGameObjectsWithTag("MachineGun");
    }
    public void TakeDamage()
    {
        health -= 1f;
        if (health <= 0) { GameOver(); }
    }

    void PrintHealthAndMoney()
    {
        moneyText.text = money.ToString() + "$";
        healthText.text = health.ToString();
    }

    void SendEnemies()
    {
        if (enemies.Length == 0 && isDone)
        {
            int quantity = Random.Range(minEnemyNumberPerSpawn, maxEnemyNumberPerSpawn);
            StartCoroutine(SendEnemyWave(quantity));
            minEnemyNumberPerSpawn += 1;
            maxEnemyNumberPerSpawn += 1;
        }
    }

    IEnumerator SendEnemyWave(int quantity)
    {
        isDone = false;
        for (int i = 0; i < quantity; i++)
        {
            route = Random.Range(0, 3);
            GameObject enemyClone;
            if (route == 0)
            {
                enemyClone = Instantiate(enemy, enemyRoute1[0].position, Quaternion.identity);
                enemyClone.GetComponent<Enemy>().route = 0;
            }
            else if (route == 1)
            {
                enemyClone = Instantiate(enemy, enemyRoute2[0].position, Quaternion.identity);
                enemyClone.GetComponent<Enemy>().route = 1;
            }
            else if (route == 2)
            {
                enemyClone = Instantiate(enemy, enemyRoute3[0].position, Quaternion.identity);
                enemyClone.GetComponent<Enemy>().route = 2;
            }
            yield return new WaitForSeconds(0.5f);
        }
        isDone = true;
    }

    void GameOver()
    {
        isDone = false;
        StopAllCoroutines();
        DestroyEnemies();
        DestroyWeapons();
        gameOverObject.SetActive(true);
    }


    void DestroyEnemies()
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }

    void DestroyWeapons()
    {
        for(int i = 0; i < machineGuns.Length; i++)
        {
            Destroy(machineGuns[i]);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
