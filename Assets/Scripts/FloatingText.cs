using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 1f;
    public Vector3 offset = new Vector3(0,1.3f,0);
    public Transform cam;
    public Vector3 Randomize = new Vector3(1, 0, 0);
    // Start is called before the first frame update
    void Start()
    {   
        transform.LookAt(cam);
        Destroy(gameObject, DestroyTime);
        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-Randomize.x, Randomize.x),
        Random.Range(-Randomize.y , Randomize.y),
        Random.Range(-Randomize.z , Randomize.z));
    }
    void Update()
    {
        
    }
}
