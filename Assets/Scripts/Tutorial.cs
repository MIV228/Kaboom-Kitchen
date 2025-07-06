using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameController controller;
    public bool night;
    public GameObject child;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (night && controller.currTime == GameController.Time.Night)
        {
            child.gameObject.SetActive(true);
        }
        if (controller.currDay > 1)
        {
            Destroy(gameObject);
        }
    }
}
