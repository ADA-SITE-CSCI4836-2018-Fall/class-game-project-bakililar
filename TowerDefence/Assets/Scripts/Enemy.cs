using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float maxDeltaAcc = .5f;

    private float speed = 7f;

    public Light LLight;
    public Light RLight;

    private Transform target;
    private int wavePointIndex = 0;

    public float startHealth = 100;
    private float health;

    public int reward = 50;

    public GameObject deathEffect;

    public AudioClip[] sounds;

    [Header("Unity Stuff")]
    public Image healthBar;

    // Use this for initialization
    void Start()
    {
        health = startHealth;

        target = Waypoints.points[0];

        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = sounds[Random.Range(0, sounds.Length - 1)];
        audioSource.volume = Random.Range(0, 1f);
        audioSource.time = Random.Range(0, audioSource.clip.length / 2);
        audioSource.PlayDelayed((ulong)(Random.Range(0, 2f)));
    }

    private float duration = 2f;


    public void takeDamage(int amount)
    {

        health -= amount;

        healthBar.fillAmount = health / startHealth;

        Debug.Log("Left: " + health);

        if (health <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("DIE");
        PlayerStats.money += reward;
        WaveSpawner.enemiesAlive--;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        //healthBar.transform.LookAt(Camera.main.transform);

        Vector3 dir = (target.position - transform.position).normalized;

        transform.Translate(dir * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 1f)
        {
            GetNextWayPoint();
        }

        float r = Random.Range(0f, 5f);

        if (r < 0.1)
            margat();

        duration -= Time.deltaTime;

        if (duration <= 0)
            notMargat();

    }

    void margat()
    {
        LLight.intensity = Random.Range(5, 10);
        RLight.intensity = Random.Range(5, 10);
        duration = Random.Range(.1f, .3f);
    }

    void notMargat()
    {
        LLight.intensity = 1;
        RLight.intensity = 1;
    }

    void GetNextWayPoint()
    {
        if (wavePointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavePointIndex++;
        target = Waypoints.points[wavePointIndex];
        transform.LookAt(target);
    }

    void EndPath()
    {
        PlayerStats.lives--;
        Destroy(gameObject);

        WaveSpawner.enemiesAlive--;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
    }
}
