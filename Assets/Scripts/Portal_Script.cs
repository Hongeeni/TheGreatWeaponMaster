using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_Script : MonoBehaviour
{
    public string goToStage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerPrefs.SetString("PlayStage", goToStage);
            LoadScene_Script.LoadScene(goToStage);
        }
    }
}
