using UnityEngine;
using TMPro;

public class DestroyCube : MonoBehaviour
{
    private Renderer renderer;
    public GameObject hitSoundObj;

    private void Awake()
    {
        // Sets renderer to the renderer component on Awake()
        renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // I used OnTriggerEnter, as OnCollisionEnter was not working well
    private void OnTriggerEnter(Collider other)
    {
        Renderer otherRenderer = other.GetComponent<Renderer>();

        // Checks if current GameObject's material is the same as the collided object.
        // This works because only the "sabers" and the cubes will have the Red and Blue materials.
        if (renderer.sharedMaterial == otherRenderer.sharedMaterial)
        {
            Instantiate(hitSoundObj, other.transform.position, transform.rotation);
            Destroy(other.gameObject);

            // Change Score
            GameManager.instance.blockScore++;
            GameManager.instance.scoreText.text = "Score: " + GameManager.instance.blockScore;
        }

    }
}
