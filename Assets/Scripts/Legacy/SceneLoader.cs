using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Load(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
