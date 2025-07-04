using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, GetComponent<AudioSource>().clip.length + 0.1f);
    }
}
