using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifetime;
    public float kb;

    public Transform lookPoint;

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            lookPoint.LookAt(other.transform);
            other.GetComponent<Rigidbody>().AddForce(lookPoint.forward * kb, ForceMode.Impulse);
        }
    }
}
