using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider))]
public class ShopButton : MonoBehaviour
{
    public Shop shop;
    public Money money;

    public InputActionProperty r_grabAction;
    public InputActionProperty l_grabAction;

    public string type;

    public float cd;

    public bool use;

    private void Start()
    {
        shop = FindObjectOfType<Shop>();
        money = FindObjectOfType<Money>();
    }

    private void Update()
    {
        cd -= Time.deltaTime;
        if (use)
        {
            use = false;
            Action();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "RightHand")
        {
            if (r_grabAction.action.ReadValue<float>() > 0)
            {
                Action();
            }
        }
        else if (other.tag == "LeftHand")
        {
            if (l_grabAction.action.ReadValue<float>() > 0)
            {
                Action();
            }
        }
    }

    public void Action()
    {
        if (cd > 0) return;

        cd = 1;
        if (type == "reroll")
        {
            shop.Reroll();
            Instantiate(shop.purchase_sound, transform.position, Quaternion.identity);
        }
        else if (type == "upgrade")
        {
            Consumable c = GetComponent<Consumable>();
            ShopItem item = GetComponent<ShopItem>();
            if (money.money >= item.cost)
            {
                money.money -= item.cost;
                if (c.damage != 0) shop.playerController.currDamage += c.damage;
                if (c.xdamage != 0) shop.playerController.currDamage = Mathf.RoundToInt(shop.playerController.currDamage * c.xdamage);
                if (c.ap != 0) shop.playerController.currArmorPierce += c.ap;
                if (c.xap != 0) shop.playerController.currArmorPierce = Mathf.RoundToInt(shop.playerController.currArmorPierce * c.xap);
                Instantiate(shop.purchase_sound, transform.position, Quaternion.identity);
                shop.playerController.UpdateValues();
                Destroy(gameObject);
            }
        }
        else if (type == "exit")
        {
            FindObjectOfType<GameController>().StartNight();
        }
    }
}
