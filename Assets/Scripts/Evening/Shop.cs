using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Diagnostics.Contracts;

public class Shop : MonoBehaviour
{
    public ShopItem[] upgrades;

    public ContractManager contractManager;

    public List<Vector3> item_points = new List<Vector3>();
    public List<TMP_Text> price_tags = new List<TMP_Text>();
    public GameObject price_tag_prefab;
    public Transform item_holder;
    public Transform price_tag_holder;
    public int item_count = 4;

    public int reroll_cost = 5;
    public TMP_Text reroll_text;

    public Player playerController;
    public GameController globalRoomController;

    void Start()
    {
        playerController = FindObjectOfType<Player>();
        globalRoomController = FindObjectOfType<GameController>();
        contractManager = FindObjectOfType<ContractManager>();

        upgrades = Resources.LoadAll("Items/Upgrades", typeof(ShopItem)).Cast<ShopItem>().ToArray();

        for (int i = 0; i < item_count; i++)
        {
            item_points.Add(new Vector3(1.3f * i - (1.3f * (item_count - 1) / 2), 0, 0));
            GameObject g = Instantiate(price_tag_prefab, price_tag_holder);
            g.transform.localPosition = new Vector3(1.3f * i - (1.3f * (item_count - 1) / 2), 0, 0);
            price_tags.Add(g.transform.GetChild(0).GetComponent<TMP_Text>());
        }
        GenerateStock();
    }

    public void Reroll()
    {
        reroll_cost += 1;
        reroll_text.text = reroll_cost.ToString() + "$";
        foreach (Transform item in item_holder.GetComponentsInChildren<Transform>())
        {
            if (item != item_holder) Destroy(item.gameObject);
        }
        GenerateStock();
    }

    void GenerateStock()
    {
        for (int i = 0; i < item_count; i++)
        {
            int random_item = Random.Range(0, upgrades.Length);
            //!curr_stock.Contains(all_items[random_item]) && !new_stock.Contains(all_items[random_item]) &&
            //while (upgrades[random_item].min_door > globalRoomController.currentDoorNumber) random_item = Random.Range(0, upgrades.Length);
            Transform new_item = Instantiate(upgrades[random_item].item, item_holder).transform;
            new_item.localPosition = item_points[i];
        }
    }
}