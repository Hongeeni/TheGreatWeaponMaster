using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public bool takeDamage = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss" && !takeDamage)
        {
            other.transform.root.GetComponent<Enemy>().TakeDamage(0.07f);
            takeDamage = true;
        }
        else if (other.tag == "MiddleBoss" && !takeDamage)
        {
            other.transform.root.GetComponent<Enemy>().TakeDamage(0.12f);
            takeDamage = true;
        }
        else if (other.tag == "Enemy" && !takeDamage)
        {
            other.transform.root.GetComponent<Enemy>().TakeDamage(0.34f);
            takeDamage = true;
        }
    }
}
