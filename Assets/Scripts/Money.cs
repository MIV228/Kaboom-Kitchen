using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int money;
    public TMP_Text text;

    private void Update()
    {
        text.text = money.ToString() + "$";
    }
}
