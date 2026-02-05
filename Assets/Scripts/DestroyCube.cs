using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    private Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Renderer otherRenderer = other.GetComponent<Renderer>();
        if (renderer.sharedMaterial == otherRenderer.sharedMaterial)
            Destroy(other.gameObject);
    }
}
