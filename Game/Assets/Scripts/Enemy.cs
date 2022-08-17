using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public bool route;
    public float health;
    public float deadEnemyValue;
    private int stationInd;
    public int finalStationInd;
    public Transform currentStation;
    public Slider healthSlider;
    void Start()
    {
        health = 100f;
        stationInd = 1;
        if (route)
        {
            finalStationInd = GameManager.instance.enemyRoute1.Length;
            currentStation = GameManager.instance.enemyRoute1[stationInd];
        }
        else
        {
            finalStationInd = GameManager.instance.enemyRoute2.Length;
            currentStation = GameManager.instance.enemyRoute2[stationInd];
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
            if (route)
            {
                currentStation = GameManager.instance.enemyRoute1[stationInd];
            }
            else
            {
                currentStation = GameManager.instance.enemyRoute2[stationInd];
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
        if (health <= 0) 
        { 
            Destroy(gameObject); 
            health = 0f;
            GameManager.instance.money += deadEnemyValue;
            return; }
        healthSlider.value = health / 100f;
    }
}
