using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickStart : MonoBehaviour
{
    Rigidbody myBod;
    public Vector3 kick;
    // Start is called before the first frame update
    void Start()
    {
        myBod = GetComponent<Rigidbody>();
        myBod.velocity = kick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
