using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoolButton : MonoBehaviour
{
    public bool play;

    private void Update()
    {
        if (play) Play();
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
