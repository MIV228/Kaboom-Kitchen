using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public int damage;
    public float xdamage;
    public int ap;
    public float xap;

    public TMP_Text text;
    public string description;

    private void Start()
    {
        text.text = description + "\n" + GetComponent<ShopItem>().cost.ToString() + "$";
    }
}