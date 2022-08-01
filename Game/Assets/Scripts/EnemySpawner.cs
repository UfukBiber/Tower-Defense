using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public float frequency;
    public int maxNumberInOneTime;
    private float nextWaveTime;
    public float maxDurationBetweenWaves;
    public float minDurationBetweenSpawns;
    private bool done = true;

    public bool isPaused = false;

    private void Update()
    {
        if (!isPaused)
        {
            if (Time.time > nextWaveTime)
            {
                SendNewWave();
            }
        }
        else
        {
            StopAllCoroutines();
            done = true;
            nextWaveTime = Time.time + 1f;
        }
    }
    void SendNewWave()
    {
        if (done && Time.time > nextWaveTime)
        {
            done = false;
            int howMany = Random.Range(1, maxNumberInOneTime); 
            StartCoroutine(sendEnemy(howMany));
        }
    }
    private IEnumerator sendEnemy(int howMany)
    {
        for (int i = 0; i < howMany; i++)
        {
            Instantiate(Enemy, transform.position, transform.rotation);
            yield return new WaitForSeconds(frequency);
        }
        done = true;
        nextWaveTime = Time.time + Random.Range(minDurationBetweenSpawns, maxDurationBetweenWaves);
    }
}
