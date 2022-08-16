using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float deadEnemyValue;
    private int stationInd;
    public int finalStationInd;
    public Transform currentStation;
    public Slider healthSlider;
    void Start()
    {
        health = 100f;
        stationInd = 0;
        finalStationInd = GameManager.instance.stations.Length;
        currentStation = GameManager.instance.stations[stationInd];

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
            currentStation = GameManager.instance.stations[stationInd];
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
