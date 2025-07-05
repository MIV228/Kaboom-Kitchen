using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Daytime : MonoBehaviour
{
    public GameController gc;

    public float timer;
    public int currentDay;
    public int shaurmaWeight;
    public Shaurma shaurma;
    public Transform shaurmaHolder;
    public Shaurma currShaurma;
    public bool Cooking = false;

    public Money money;

    public GameObject[] customers;
    public int lastCustomer;
    public Transform customerHolder;
    public Transform shaurma_destination;

    public TaskView taskView;

    [System.Serializable]
    public class Product
    {
        public int ID;
        public string Name;
        public Sprite sprite;
        public GameObject model;
        public int MinimalWaveToSpawn;
    }

    public Product[] allProducts;

    public Dictionary<int, int> currentTask = new Dictionary<int, int>();

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
        money = FindObjectOfType<Money>();
    }

    public void StartDay()
    {
        timer = 0;
        Cooking = true;
        GenerateShaurma();
    }

    private void Update()
    {
        if (gc.currTime != GameController.Time.Day) return;

        timer += Time.deltaTime;
        if (timer >= 120 && !Cooking) 
        {
            EndDay();
        }
    }

    public void GenerateShaurma()
    {
        List<Product> availableProducts = new List<Product>();
        foreach (Product product in allProducts)
        {
            if (product.MinimalWaveToSpawn <= currentDay) { 
                availableProducts.Add(product);
            }
        }

        shaurmaWeight = currentDay * 3;
        Dictionary<int, int> newShaurma = new Dictionary<int, int>();

        foreach (Product product in availableProducts)
        {
            int productAmount = Random.Range(0, currentDay + 1);
            if (productAmount > 0) newShaurma.Add(product.ID, productAmount);
        }
        currentTask.Clear();
        currentTask = newShaurma;
        taskView.UpdateView();

        foreach (Transform c in customerHolder)
        {
            if (c != customerHolder) Destroy(c.gameObject);
        }
        foreach (Transform c in shaurmaHolder)
        {
            if (c != shaurmaHolder) Destroy(c.gameObject);
        }

        currShaurma = Instantiate(shaurma, shaurmaHolder);
        int r = Random.Range(0, customers.Length);
        while (r == lastCustomer) r = Random.Range(0, customers.Length);
        Instantiate(customers[r], customerHolder);
    }

    public void DeleteShaurma()
    {
        Destroy(currShaurma);
        currShaurma = Instantiate(shaurma, shaurmaHolder);
    }

    public void CheckShaurma()
    {
        int mistakes = 0;
        foreach (KeyValuePair<int, int> pair in currShaurma.thisShaurma)
        {
            if (currentTask.ContainsKey(pair.Key))
            {
                if (currentTask[pair.Key] != pair.Value)
                {
                    mistakes++;
                }
            }
            else mistakes++;
        }
        int m = currShaurma.thisShaurma.Count - mistakes;
        if (m < 0) m = 0;
        money.money += m;
        if (timer >= 120) Cooking = false;
        else GenerateShaurma();
    }

    public void EndDay()
    {
        foreach (Transform c in customerHolder)
        {
            if (c != customerHolder) Destroy(c.gameObject);
        }
        gc.StartNight();
    }
}