using System;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    // Instead of using a material array, I made two prefabs, a blue and red cube.
    public float bpm;
    public float secondsPerBeat;

    public GameObject[] cubes = new GameObject[2];

    void Start()
    {
        secondsPerBeat = 60f / bpm;
        InvokeRepeating("MakeBlock", secondsPerBeat, secondsPerBeat);
    }

    void Update()
    {
        
    }

    void MakeBlock()
    {
        // I used Random.Range to randomly select a block instead of using a ChangeColor function
        GameObject spawnBlock = Instantiate(cubes[UnityEngine.Random.Range(0, 2)], gameObject.transform);

        Rigidbody rb = spawnBlock.GetComponent<Rigidbody>();
        rb.linearVelocity = new Vector3(0, 0, -1);
    }
}
