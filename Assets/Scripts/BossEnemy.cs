using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : MonoBehaviour
{
    public Transform target;
    public Transform projectilePoint;
    public GameObject projectile;

    private AudioSource musicPlayer;
    public AudioClip Fireball;

    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        transform.LookAt(target);
        musicPlayer.clip = Fireball;
        musicPlayer.Play();
        Rigidbody rb = Instantiate(projectile, projectilePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        float distance = Vector3.Distance(target.position, projectilePoint.transform.position);
        rb.AddForce(this.transform.forward * distance * 1.7f, ForceMode.Impulse);
    }
}
