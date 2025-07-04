using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int armor;

    public GameObject model;
    public GameObject ragdoll;

    public float runSpeed;

    public int ai_ID;

    public Transform target;
    public Rigidbody rb;

    public float attackCD;
    public float attackCDTimer;
    public bool isAttacking;
    public float minAttackDistance;
    public GameObject attackHitbox;

    public float stunTime;

    void Start()
    {
        target = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (stunTime <= 0)
        {
            if (!isAttacking)
            {
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                rb.AddForce(transform.forward * runSpeed * Time.deltaTime, ForceMode.Force);
            }
            if (Vector3.Distance(transform.position, target.position) <= minAttackDistance) StartCoroutine(Attack());
        }
        else
        {
            stunTime -= Time.deltaTime;
        }

        attackCDTimer -= Time.deltaTime;
        if (health <= 0) Die();
    }

    public void Hurt(int damage, int armorPierce)
    {
        int a = armor - armorPierce;
        if (a < 0) a = 0;
        int d = damage - a;
        if (d < 0) d = 1;
        health -= d;
        stunTime = 1;
        rb.AddForce(-transform.forward, ForceMode.Impulse);
    }

    public void Die()
    {
        model.SetActive(false);
        ragdoll.SetActive(true);
    }

    public IEnumerator Attack()
    {
        isAttacking = true;
        attackCDTimer = attackCD;
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        attackHitbox.SetActive(false);
        isAttacking = false;
    }
}
