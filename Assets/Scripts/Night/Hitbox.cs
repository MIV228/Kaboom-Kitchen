using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int damage;
    public int armorPierce;
    public bool isPlayerFriendly;

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
    }
}
