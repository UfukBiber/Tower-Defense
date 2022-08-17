using UnityEngine;
using System.Collections;
using TMPro;
public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    public Transform[] enemyRoute1;
    public Transform[] enemyRoute2;
    private int route; // if 0 route1 is selected; if 1 route2 is selected;
    public GameObject[] shops;
    public GameObject[] enemies;
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

    public void TakeDamage()
    {
        health -= 1f;

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
            route = Random.Range(0, 2);
            GameObject enemyClone;
            if (route == 1)
            {
                enemyClone = Instantiate(enemy, enemyRoute1[0].position, Quaternion.identity);
                enemyClone.GetComponent<Enemy>().route = true;
            }
            else if (route == 0)
            {
                enemyClone = Instantiate(enemy, enemyRoute2[0].position, Quaternion.identity);
                enemyClone.GetComponent<Enemy>().route = false;
            }
            yield return new WaitForSeconds(0.5f);
        }
        isDone = true;
    }

}
