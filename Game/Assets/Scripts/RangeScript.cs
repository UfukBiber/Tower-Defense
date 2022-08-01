using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;


    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("GameOver");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        TakeDamage(10);
        Destroy(other.gameObject);
    }
}
