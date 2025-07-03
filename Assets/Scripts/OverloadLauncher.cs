using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class OverloadLauncher : MonoBehaviour
{
    public GameObject rocket;
    public Transform attackPoint;
    public InputActionProperty attackAction;
    public InputActionProperty abilityAction;
    public float maxcd;
    public float cd;
    public float maxshootcd;
    public float shootcd;
    public float maxloadcd;
    public float loadcd;

    public TMP_Text text;

    public int rockets;

    public AudioObject attackSource;
    public AudioObject loadSource;

    public void Shoot()
    {
        Instantiate(rocket, attackPoint.position, transform.rotation);
        Instantiate(attackSource, attackPoint.position, transform.rotation);
        rockets -= 1;
        shootcd = 0;
    }

    public void LoadRockets()
    {
        rockets += 1;
        Instantiate(loadSource, attackPoint.position, transform.rotation);
        loadcd = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cd < maxcd)
        {
            cd += Time.deltaTime;
        }
        if (shootcd < maxshootcd)
        {
            shootcd += Time.deltaTime;
        }
        if (loadcd < maxloadcd)
        {
            loadcd += Time.deltaTime;
        }
        float triggerValue = attackAction.action.ReadValue<float>();
        if (triggerValue > 0)
        {
            if (loadcd >= maxloadcd && rockets < 4) LoadRockets();
        }
        else
        {
            if (shootcd >= maxshootcd && rockets > 0) Shoot();
        }
        float triggerValueA = abilityAction.action.ReadValue<float>();
        if (triggerValueA > 0 && cd >= maxcd)
        {
            rockets += 2;
            cd = 0;
        }
        text.text = rockets.ToString();
    }
}
