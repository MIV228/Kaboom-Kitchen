using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class StockLauncher : MonoBehaviour
{
    public GameObject rocket;
    public Transform attackPoint;
    public InputActionProperty attackAction;
    public float maxcd;
    public float cd;

    public AudioObject attackSource;

    public void Shoot()
    {
        Instantiate(rocket, attackPoint.position, transform.rotation);
        Instantiate(attackSource, attackPoint.position, transform.rotation);
        cd = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cd < maxcd)
        {
            cd += Time.deltaTime;
        }
        float triggerValue = attackAction.action.ReadValue<float>();
        if (triggerValue > 0 && cd >= maxcd)
        {
            Shoot();
        }
    }
}
