using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public Transform enemyToFire;
    public Transform barrelContainer;
    public float range;
    public float fireRate;
    public float nextTimeToFire;
    public int weaponDamage;
    public ParticleSystem barrel_1;
    public ParticleSystem barrel_2;
    void Start()
    {
        barrel_1.Stop();
        barrel_2.Stop();
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
            barrelContainer.LookAt(enemyToFire.position);
            enemyToFire.GetComponent<Enemy>().TakeDamage(weaponDamage);
            nextTimeToFire = Time.time + fireRate;
            StartParticleSystems();
        }
        else { StopParticles(); }
    }

    void StartParticleSystems()
    {
        barrel_1.Play();
        barrel_2.Play();
    }

    void StopParticles()
    {
        barrel_1.Stop();
        barrel_2.Stop();
    }

    public void Upgrade()
    {
        range += 1f;
        weaponDamage += 5;
    }
}
