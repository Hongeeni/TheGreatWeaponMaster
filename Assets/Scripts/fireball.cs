using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    public GameObject obj;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Boss" && collision.transform.tag != "Player")
        {
            Destroy(obj);
        }
    }
}
