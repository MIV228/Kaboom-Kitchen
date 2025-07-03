using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) return;

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
