using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider))]
public class ProductBox : MonoBehaviour
{
    public GameObject Food;
    public Transform r_FoodHolder;
    public Transform l_FoodHolder;
    public InputActionProperty r_grabAction;
    public InputActionProperty l_grabAction;

    private void Start()
    {
        r_FoodHolder = GameObject.Find("r_FoodHolder").transform;
        l_FoodHolder = GameObject.Find("l_FoodHolder").transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "RightHand")
        {
            if (r_FoodHolder.childCount == 0 && r_grabAction.action.ReadValue<float>() > 0)
            {
                Instantiate(Food, r_FoodHolder);
            }
        }
        else if (other.tag == "LeftHand")
        {
            if (l_FoodHolder.childCount == 0 && l_grabAction.action.ReadValue<float>() > 0)
            {
                Instantiate(Food, l_FoodHolder);
            }
        }
    }
}
