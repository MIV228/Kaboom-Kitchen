using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public float speed;

    public int currDamage;
    public int currArmorPierce;

    public Transform katanaPlane;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        
    }

    public void Hurt(int damage)
    {
        health -= damage;
    }
}
