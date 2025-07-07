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
    public Hitbox hitbox;

    public TMP_Text healthText;
    public TMP_Text damageText;
    public TMP_Text apText;

    void Start()
    {
        health = maxHealth;
        UpdateValues();
    }

    void Update()
    {
        healthText.text = health.ToString() + "/" + maxHealth.ToString() + " HP";
    }

    public void UpdateValues()
    {
        damageText.text = currDamage.ToString() + " урона";
        apText.text = currArmorPierce.ToString() + " пробивания";
        hitbox.damage = currDamage;
        hitbox.armorPierce = currArmorPierce;
    }

    public void Hurt(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    public void Die()
    {
        SceneManager.LoadScene(2);
    }
}
