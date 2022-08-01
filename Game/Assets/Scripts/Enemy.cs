using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public float health;
    public Slider slider;
    public float damage;
    public float speed;
    private int currentTargetInd;

    public bool isPaused = false;

    void Update()
    {
        slider.value = health / 100;
        if (!isPaused)
        {
            if (currentTargetInd < Destinations.destinations.Length - 1)
            {
                IsArrived();
            }
            else { Destroy(gameObject, 1f); }
            Walk();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void IsArrived()
    {
        if ((transform.position - Destinations.destinations[currentTargetInd].transform.position).magnitude <= 0.1f)
        {
            
            currentTargetInd += 1;
      
        }
    }

    private void Walk()
    {
        Vector3 dir = (Destinations.destinations[currentTargetInd].transform.position - transform.position).normalized; 
        dir = dir * speed * Time.deltaTime;
        transform.Translate(dir);
    }
}
