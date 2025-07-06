using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public float speed;

    public int currDamage;
    public int currArmorPierce;

    public Transform katanaPlane;

    public TMP_Text healthText;
    public TMP_Text damageText;
    public TMP_Text apText;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        healthText.text = health.ToString() + "/" + maxHealth.ToString() + "здоровья";
        damageText.text = currDamage.ToString() + " урона";
        apText.text = currArmorPierce.ToString() + " пробивания";
    }

    public void Hurt(int damage)
    {
        health -= damage;
    }

    public IEnumerator Die()
    {

        yield return null;
    }
}
