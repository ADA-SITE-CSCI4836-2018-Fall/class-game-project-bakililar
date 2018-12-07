using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class WaveSpawner : MonoBehaviour
{


    public Transform enemyPrefab;

    public Transform spawnPoint;

    public float timeBetweeenWaves = 5f;

    private float countdown = 2f;

    public Text waveCountdownText;

    private int waveindex = 0;

    public static int enemiesAlive = 0;

    // Update is called once per frame
    void Update()
    {
        if (WaveSpawner.enemiesAlive > 0)
            return;

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweeenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        waveindex++;

        PlayerStats.rounds++;

        for (int i = 0; i < waveindex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
    }


    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
