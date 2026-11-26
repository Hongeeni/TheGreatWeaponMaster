using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Player_Script : MonoBehaviour
{
    private Rigidbody playerRigidbody;

    private float playerSpeed = 10.0f;
    private float playerJumpForce = 10.0f;

    GameObject nearObject;
    bool iDown;
    public GameObject[] weapons;
    public bool[] hasWeapons;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            Move();

            Jump();
        }
        Interation();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(0, 0, playerSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(0, 0, playerSpeed * Time.deltaTime * -1f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(playerSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(playerSpeed * Time.deltaTime * -1f, 0, 0);
        }
    }

    private void Jump()
    {
        if (playerRigidbody.velocity.y == 0 && Input.GetKey(KeyCode.Space))
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, playerRigidbody.velocity.y + playerJumpForce, playerRigidbody.velocity.z);
        }
    }

    void GetInput()
    {
        iDown = Input.GetButtonDown("Interation");
    }

    void Interation()
    {
        if(iDown&&nearObject!=null)
        {
            if(nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Weapon")
            nearObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Weapon")
            nearObject = null;
    }
}
