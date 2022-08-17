using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public Transform enemyToFire;
    public Transform barrelContainer;
    public float range;
    public float fireRate;
    public float nextTimeToFire;
    public int weaponDamage;
    public ParticleSystem[] barrels;
    void Start()
    {
        StopParticles();
    }

    // Update is called once per frame
    void Update()
    {
        DetectEnemy();
        Shoot();
    }

    void DetectEnemy()
    {
        if (enemyToFire == null)
        {
            float distance = 100.0f;
            for (int i = 0; i < GameManager.instance.enemies.Length; i++)
            {
                float enemyDistance = Vector3.Distance(GameManager.instance.enemies[i].transform.position, transform.position);
                if (enemyDistance < range && enemyDistance < distance)
                {
                    enemyToFire = GameManager.instance.enemies[i].transform;
                }
            }
        }
        else
        {
            if (Vector3.Distance(enemyToFire.position, transform.position) < range)
            {
                enemyToFire = null;
            }
        }
    }

    void Shoot()
    {
        if (enemyToFire != null && Time.time >= nextTimeToFire)
        {
            barrelContainer.LookAt(enemyToFire.position + new Vector3(0f, 1f, 0f));
            enemyToFire.GetComponent<Enemy>().TakeDamage(weaponDamage);
            nextTimeToFire = Time.time + fireRate;
            StartParticleSystems();
        }
        else { StopParticles(); }
    }

    void Aim()
    {

    }
    void StartParticleSystems()
    {
        for (int i = 0; i < barrels.Length; i++)
        {
            barrels[i].Play();
        }
    }

    void StopParticles()
    {
        for (int i = 0; i < barrels.Length; i++)
        {
            barrels[i].Stop();
        }
    }

    public void Upgrade()
    {
        range += 1f;
        weaponDamage += 5;
    }
}
