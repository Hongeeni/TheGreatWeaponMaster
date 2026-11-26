using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Grenade, DamageUp, Health, RecoverSpeed, AttackSpeed, Weapon};
    public Type type;
    public int value;

    Rigidbody rigid;
    SphereCollider sphereCollider;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }
    void Update()
    {
        //transform.Rotate(Vector3.up * 30 * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision) //물리효과 변경 가즈아
    {
        if(collision.gameObject.tag == "Floor")
        {
            rigid.isKinematic = true;
            sphereCollider.enabled = false;
        }
    }
}