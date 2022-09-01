using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int route;
    public float health;
    public float deadEnemyValue;
    private int stationInd;
    public int finalStationInd;
    public Transform currentStation;
    public Slider healthSlider;

    public Material original;
    public Material damage;
    
    void Start()
    {
        health = 100f;
        stationInd = 1;
        if (route == 0)
        {
            finalStationInd = GameManager.instance.enemyRoute1.Length;
            currentStation = GameManager.instance.enemyRoute1[stationInd];
        }
        else if (route == 1)
        {
            finalStationInd = GameManager.instance.enemyRoute2.Length;
            currentStation = GameManager.instance.enemyRoute2[stationInd];
        }
        else if (route == 2)
        {
            finalStationInd = GameManager.instance.enemyRoute3.Length;
            currentStation = GameManager.instance.enemyRoute3[stationInd];
        }
        else if (route == 3)
        {
            finalStationInd = GameManager.instance.enemyRoute4.Length;
            currentStation = GameManager.instance.enemyRoute4[stationInd];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        IsArrived();
        GoToCurrentStation();
    }

    void IsArrived()
    {
        if ((currentStation.position - transform.position).magnitude < 0.2f)
        {
            stationInd = (stationInd + 1);
            if (stationInd == finalStationInd)
            {
                GameManager.instance.TakeDamage();
                Destroy(gameObject);
                return;
            }
            if (route == 0)
            {
                currentStation = GameManager.instance.enemyRoute1[stationInd];
            }
            else if (route == 1)
            {
                currentStation = GameManager.instance.enemyRoute2[stationInd];
            }
            else if (route == 2)
            {
                currentStation = GameManager.instance.enemyRoute3[stationInd];
            }
            else if (route == 3)
            {
                currentStation = GameManager.instance.enemyRoute4[stationInd];
            }
        }
    }
    void GoToCurrentStation()
    {
        transform.LookAt(currentStation.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(changeColor());
        if (health <= 0)
        {
            gameObject.tag = "DeadEnemy";
            speed = 0f;
            Destroy(gameObject, 0.05f);
            health = 0f;
            GameManager.instance.money += deadEnemyValue;
        }
        healthSlider.value = health / 100f;
    }

    IEnumerator changeColor()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();
        renderer.material = damage;
        yield return new WaitForSeconds(1f);
        renderer.material = original;
    }
}
