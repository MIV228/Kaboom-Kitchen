using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Hitbox : MonoBehaviour
{
    public int damage;
    public int armorPierce;
    public bool isPlayerFriendly;

    public Player player;

    public float sliceCD;
    public Material crossSectionMaterial;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!isPlayerFriendly)
            {
                Player player = other.gameObject.GetComponent<Player>();
                player.Hurt(damage);
            }
        }
        else if (other.tag == "Enemy")
        {
            if (isPlayerFriendly)
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                enemy.Hurt(damage, armorPierce);
            }
        }
        else if (other.gameObject.layer == 6 && sliceCD <= 0)
        {
            SlicedHull hull = other.gameObject.Slice(player.katanaPlane.position, player.katanaPlane.up);

            if (hull != null)
            {
                GameObject upper_hull = hull.CreateUpperHull(other.gameObject, crossSectionMaterial);
                GameObject lower_hull = hull.CreateLowerHull(other.gameObject, crossSectionMaterial);

                sliceCD = 0.2f;

                upper_hull.AddComponent<Rigidbody>();
                upper_hull.AddComponent<MeshCollider>();
                upper_hull.GetComponent<MeshCollider>().convex = true;
                upper_hull.GetComponent<MeshCollider>().providesContacts = true;
                upper_hull.GetComponent<Rigidbody>().AddForce(player.katanaPlane.up, ForceMode.Impulse);
                upper_hull.layer = 6;
                upper_hull.AddComponent<EnemyCorpse>();

                lower_hull.AddComponent<Rigidbody>();
                lower_hull.AddComponent<MeshCollider>();
                lower_hull.GetComponent<MeshCollider>().convex = true;
                lower_hull.GetComponent<MeshCollider>().providesContacts = true;
                upper_hull.GetComponent<Rigidbody>().AddForce(-player.katanaPlane.up, ForceMode.Impulse);
                lower_hull.layer = 6;
                lower_hull.AddComponent<EnemyCorpse>();

                Destroy(other.gameObject);
            }
            else print("no");
        }
    }

    private void Update()
    {
        if (sliceCD > 0) sliceCD -= Time.deltaTime;
    }
}
