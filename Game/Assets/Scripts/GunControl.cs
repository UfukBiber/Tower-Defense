using UnityEngine;
using UnityEngine.UI;
public class GunControl : MonoBehaviour
{
    public GameObject[] ExitLocation;
    public Canvas weaponMenu;
    private bool weaponMenuOpen;
    public float gunDamage;
    public float bulletExitSpeed;
    public float range;
    public GameObject barrel;
    public GameObject muzzle;
    private GameObject currentMuzzle;
    public GameObject[] Enemies;
    public GameObject currentEnemy;


    public float fireRate;
    private float nextFireTime;
    
    private int i = 0;

    void Update()
    {
        UpdateEnemies();
        UpdateEnemyInRange();
        Shoot();
    }

    void UpdateEnemies()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void UpdateEnemyInRange()
    {
        if (currentEnemy != null && (currentEnemy.transform.position - transform.position).magnitude > range)
        {
            currentEnemy = null;
        }

        for (int j = 0; j < Enemies.Length; j++)
        {
            if ((Enemies[j].transform.position - transform.position).magnitude <= range)
            {
                currentEnemy = Enemies[j];
                break;
            }
        }

    }
    private void OnMouseDown()
    {
        if (!weaponMenuOpen) { weaponMenu.gameObject.SetActive(true); weaponMenuOpen = true; }
        else { weaponMenu.gameObject.SetActive(false); weaponMenuOpen = false; }
        
    }

    void Shoot()
    {
        if (currentEnemy != null && Time.time >= nextFireTime)
        {
            currentMuzzle = Instantiate(muzzle, ExitLocation[i].transform.position, ExitLocation[i].transform.rotation);
            Destroy(currentMuzzle, 0.1f);
            i = (i + 1) % ExitLocation.Length;
            nextFireTime = Time.time + fireRate;
            barrel.transform.LookAt(currentEnemy.transform);
            RaycastHit hit;
            if (Physics.Raycast(barrel.transform.position, barrel.transform.forward, out hit, range))
                if (hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<Enemy>().TakeDamage(gunDamage);
                }
            }
        }
    }
 
