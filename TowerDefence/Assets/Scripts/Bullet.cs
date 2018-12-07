using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;

    public float speed = 70f;
    public GameObject impactEffect;
    public float explosionRadius = 0f;

    public int damage = 50;



    private void Start()
    {
        transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }


        Vector3 dir = target.position - transform.position;

        float distanceThisFrame = Time.deltaTime * speed;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void HitTarget()
    {
        GameObject effectinstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectinstance, 5f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        gameObject.SetActive(false);

        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }

        Destroy(gameObject, 3f);
    }

    void Damage(Transform enemy)
    {
        Debug.Log("Damage");
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
            e.takeDamage(damage);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider c in colliders)
            if (c.tag == "Enemy")
                Damage(c.transform);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
