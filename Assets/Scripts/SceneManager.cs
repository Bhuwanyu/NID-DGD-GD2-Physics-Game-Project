using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if we press R key, reload the current scene
        if(Input.GetKeyDown(KeyCode.R))
        {
            //reload the current scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
