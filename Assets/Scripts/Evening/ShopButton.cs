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

    private void Start()
    {
        shop = FindObjectOfType<Shop>();
        money = FindObjectOfType<Money>();
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
        if (type == "reroll")
        {
            shop.Reroll();
        }
        else if (type == "upgrade")
        {
            Consumable c = GetComponent<Consumable>();
            ShopItem item = GetComponent<ShopItem>();
            if (money.money >= item.cost)
            {
                money.money -= item.cost;
                shop.playerController.currDamage += c.damage;
                shop.playerController.currDamage = Mathf.RoundToInt(shop.playerController.currDamage * c.xdamage);
                shop.playerController.currArmorPierce += c.ap;
                shop.playerController.currArmorPierce = Mathf.RoundToInt(shop.playerController.currArmorPierce * c.xap);
            }
        }
        else
        {

        }
    }
}
