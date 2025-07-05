using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    public int health;
    public int armor;

    public Animator animator;
    public GameObject model;

    public float runSpeed;

    public int ai_ID;

    public Transform target;
    public Player player;
    public Rigidbody rb;

    public float attackCD;
    public float attackCDTimer;
    public bool isAttacking;
    public float minAttackDistance;
    public GameObject attackHitbox;

    public float stunTime;

    public bool isDead;
    public GameObject corpse;

    void Start()
    {
        player = FindObjectOfType<Player>();
        target = player.transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isDead) return;

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
        isDead = true;
        animator.enabled = false;
        Mesh m = new Mesh();
        model.GetComponent<SkinnedMeshRenderer>().BakeMesh(m);
        GameObject g = Instantiate(corpse, model.transform.position, model.transform.rotation);
        g.GetComponent<MeshFilter>().mesh = m;
        g.GetComponent<MeshRenderer>().materials = model.GetComponent<SkinnedMeshRenderer>().materials;
        g.AddComponent<MeshCollider>();
        g.GetComponent<MeshCollider>().convex = true;
        g.GetComponent<MeshCollider>().providesContacts = true;
        Destroy(model.GetComponent<SkinnedMeshRenderer>());
        Destroy(gameObject);
    }

    public IEnumerator Attack()
    {
        animator.SetTrigger("Attack");
        isAttacking = true;
        yield return new WaitForSeconds(0.2f);
        attackCDTimer = attackCD;
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        attackHitbox.SetActive(false);
        isAttacking = false;
    }
}
