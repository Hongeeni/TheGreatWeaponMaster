using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineActive_Script : MonoBehaviour
{
    public GameObject lineArea;
    public bool isActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isActive)
        {
            isActive = true;

            Time.timeScale = 0.0f;
            lineArea.SetActive(true);
        }
    }
}
