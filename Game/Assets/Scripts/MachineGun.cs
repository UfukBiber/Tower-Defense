using UnityEngine;

public class MachineGun : MonoBehaviour
{ 
    public Transform enemyToFire;
    public Transform turret;
    public Transform barrels;

    public ParticleSystem barrel1;
    public ParticleSystem barrel2;

    public float rotationSpeed;
    private float currentEnemyDistance;
    public float range;
    public float fireRate;
    public float nextTimeToFire;
    public int weaponDamage;
    public int sellPrice;


    private void Start()
    {
        StopParticles();
    }

    void Update()
    {
        DetectEnemy();
        RotateToEnemy();
        Shoot();
    }

    void DetectEnemy()
    {
        UpdateCurrentEnemyDistance();
        if (enemyToFire != null && currentEnemyDistance <= range)
        {
            return;
        }
        for (int i = 0; i <GameManager.instance.enemies.Length; i++)
        {
            float distanceWithEnemy = Vector3.Distance(transform.position, GameManager.instance.enemies[i].transform.position);
            if (distanceWithEnemy <= range)
            {
                enemyToFire = GameManager.instance.enemies[i].transform;
                StartParticles();
                return;
            }
        }
    }

    void UpdateCurrentEnemyDistance()
    {
        if (enemyToFire != null)
        {
            currentEnemyDistance = Vector3.Distance(enemyToFire.position, transform.position);
            if (currentEnemyDistance > range || enemyToFire.CompareTag("DeadEnemy"))
            {
                Debug.Log("Deselected");
                enemyToFire = null;
                StopParticles();
            }
        }
    }
    void RotateToEnemy()
    {
        if (enemyToFire == null) { return; }
        Vector3 relativePos = enemyToFire.position - turret.position;
        relativePos.y = 0f;
        Quaternion tempRotation =  Quaternion.LookRotation(relativePos, Vector3.up);
        turret.localRotation = Quaternion.Slerp(turret.localRotation, tempRotation, Time.deltaTime * rotationSpeed);
    }
    void Shoot()
    {
        if (enemyToFire != null && Time.time >= nextTimeToFire)
        { 
            enemyToFire.GetComponent<Enemy>().TakeDamage(weaponDamage);
            nextTimeToFire = Time.time + fireRate;
        }
    }

    public void Upgrade()
    {
        range += 1f;
        weaponDamage += 5;
    }

 

    void StartParticles()
    {
        barrel1.Play();
        barrel2.Play();
    }
    
    void StopParticles()
    {
        barrel1.Stop();
        barrel2.Stop();
    }
}
