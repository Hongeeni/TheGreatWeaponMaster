using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBasicAttack : MonoBehaviour
{
    public GameObject lineArea;
    public AudioClip BossAttack;
    private AudioSource musicPlayer;

    public static bool bossDead = false;

    // Start is called before the first frame update
    private void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (bossDead)
        {
            lineArea.SetActive(true);
            Time.timeScale = 0.0f;

            bossDead = false;
        }
    }
    void AttackSound()
    {
        musicPlayer.clip = BossAttack;
        musicPlayer.Play();
    }
}
