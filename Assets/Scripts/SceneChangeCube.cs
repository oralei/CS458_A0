using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeCube : MonoBehaviour
{
    [SerializeField] private string sceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Just like DestroyCube, I'm using OnTriggerEnter as it works better
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Saber"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void ChangeLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ChangeLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
}
