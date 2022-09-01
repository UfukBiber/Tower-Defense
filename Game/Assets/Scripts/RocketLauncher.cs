using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public Transform horizontalAxis;
    public Transform verticalAxis;

    public Transform enemyToFire;
    private float currentEnemyDistance;
    public float range;

    public GameObject bullet;
    public Transform[] rocketExitPoints;

    private int currentExitPoint;
    private GameObject currentBullet;
    private float gravity = 9.81f;
    public float rocketSpeed = 10f;
    public float angle;
    public Transform target;

    private float nextTimeToShoot;
    public float fireRate;
    public float reloadTime;


    private void Update()
    {
        DetectEnemy();
        if (enemyToFire != null)
        {
            CalculateAngle();
            Debug.Log(angle);
            Rotate();
            Shoot();
        }
    }
    void Rotate()
    {
        Vector3 relativePos = (enemyToFire.position - rocketExitPoints[currentExitPoint].position).normalized;
        relativePos.y = 0f;
        Quaternion tempRotation = Quaternion.LookRotation(relativePos, Vector3.up);
        horizontalAxis.localRotation = tempRotation;

        float verticalAngle = 90 - angle;
        verticalAxis.localRotation = Quaternion.Euler(new Vector3(verticalAngle, 0f, 0f));
    }

    void CalculateAngle()
    {
        Vector3 relativePos = enemyToFire.position - rocketExitPoints[currentExitPoint].position;
        float deltaY = -relativePos.y;
        relativePos.y = 0;
        float deltaX = relativePos.magnitude;
        float squareSpeed = rocketSpeed * rocketSpeed;
        float UnderSquareRoot = squareSpeed * squareSpeed - gravity * (gravity * deltaX * deltaX + 2 * deltaY * squareSpeed);
        if (UnderSquareRoot >= 0)
        {
            angle = squareSpeed - Mathf.Sqrt(UnderSquareRoot);
            angle = Mathf.Atan2(angle, gravity * deltaX) * Mathf.Rad2Deg;
        }

    }

    void Shoot()
    {
        if (Time.time >= nextTimeToShoot)
        {
            currentBullet = Instantiate(bullet, rocketExitPoints[currentExitPoint].position, rocketExitPoints[currentExitPoint].rotation);
            currentBullet.GetComponent<Rocket>().enemyToFire = enemyToFire;
            currentExitPoint = (currentExitPoint + 1) % rocketExitPoints.Length;
            nextTimeToShoot = Time.time + fireRate;
            if (currentExitPoint == rocketExitPoints.Length -1)
            {
                nextTimeToShoot += reloadTime;
            }
        }

    }

    void DetectEnemy()
    {
        UpdateCurrentEnemyDistance();
        if (enemyToFire != null && currentEnemyDistance <= range)
        {
            return;
        }
        for (int i = 0; i < GameManager.instance.enemies.Length; i++)
        {
            float distanceWithEnemy = Vector3.Distance(transform.position, GameManager.instance.enemies[i].transform.position);
            if (distanceWithEnemy <= range)
            {
                enemyToFire = GameManager.instance.enemies[i].transform;
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
            }
        }
    }
}
