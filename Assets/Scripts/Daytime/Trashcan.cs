using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "RightHand" || other.tag == "LeftHand")
        {
            FindObjectOfType<Daytime>().DeleteShaurma();
        }
    }
}
