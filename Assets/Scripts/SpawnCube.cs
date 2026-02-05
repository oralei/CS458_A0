using System;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    public GameObject[] cubes = new GameObject[2];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("MakeBlock", 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeBlock()
    {
        GameObject spawnBlock = Instantiate(cubes[UnityEngine.Random.Range(0, 2)], gameObject.transform);
        Rigidbody rb = spawnBlock.GetComponent<Rigidbody>();
        rb.linearVelocity = new Vector3(0, 0, -1);
    }
}
