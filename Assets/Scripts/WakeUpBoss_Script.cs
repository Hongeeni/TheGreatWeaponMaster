using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpBoss_Script : MonoBehaviour
{
    public GameObject bossMonster;
    public GameObject altar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            this.transform.position = new Vector3(0.0f, -10.0f, 0.0f);

            altar.SetActive(false);
            bossMonster.SetActive(true);
        }
    }
}
