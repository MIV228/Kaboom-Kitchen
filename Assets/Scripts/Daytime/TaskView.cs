using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TaskView : MonoBehaviour
{
    public Daytime daytime;
    public GameObject entryPrefab;
    public Transform entryHolder1;
    public Transform entryHolder2;

    public void Start()
    {
        daytime = FindObjectOfType<Daytime>();
    }

    public void UpdateView()
    {
        foreach (Transform t in entryHolder1)
        {
            if (t != entryHolder1) Destroy(t.gameObject);
        }
        foreach (Transform t in entryHolder2)
        {
            if (t != entryHolder2) Destroy(t.gameObject);
        }
        for (int i = 0; i < daytime.currentTask.Keys.Count; i++)
        {
            List<int> keys = daytime.currentTask.Keys.ToList();
            Transform t = Instantiate(entryPrefab, i % 2 == 0 ? entryHolder1 : entryHolder2).transform;
            t.localPosition = new Vector3(0, (i - i%2) / 2 * 0.06f, 0);
            t.GetChild(0).GetComponent<SpriteRenderer>().sprite = daytime.allProducts[keys[i]].sprite;
            t.GetChild(1).GetComponent<TMP_Text>().text = "x" + daytime.currentTask[keys[i]].ToString();
        }
    }
}
