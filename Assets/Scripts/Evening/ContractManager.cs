using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ContractManager : MonoBehaviour
{
    public List<int> contract_purchase_times = new List<int>();

    public Contract[] _contracts;

    private void Start()
    {
        _contracts = Resources.LoadAll("Items/Contracts", typeof(Contract)).Cast<Contract>().ToArray();
        foreach (Contract c in _contracts)
        {
            contract_purchase_times.Add(0);
        }
    }
}