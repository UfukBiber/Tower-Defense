using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float damageRange;
    public float rocketSpeed = 22f;
    public int damage;
    public Transform enemyToFire;
    

    private void Update()
    {
        if (enemyToFire != null)
        {
            transform.LookAt(enemyToFire);
            transform.Translate(Vector3.forward * Time.deltaTime * rocketSpeed);
            GiveDamage();
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    void GiveDamage()
    {
        float distance = Vector3.Distance(transform.position, enemyToFire.position);
        if (distance <= 0.5f)
        {
            for (int i = 0; i < GameManager.instance.enemies.Length; i++)
            {
                distance = Vector3.Distance(GameManager.instance.enemies[i].transform.position, transform.position);
                if (distance < damageRange)
                {
                    GameManager.instance.enemies[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            Destroy(gameObject);
        }
    }
}
