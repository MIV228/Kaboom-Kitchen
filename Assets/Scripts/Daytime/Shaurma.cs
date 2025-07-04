using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class Shaurma : MonoBehaviour
{
    public Dictionary<int, int> thisShaurma = new Dictionary<int, int>();
    public List<int> keys;
    public List<int> values;

    public Daytime daytime;

    public GameObject lavash;
    public GameObject shaurma;

    public InputActionProperty r_grabAction;
    public InputActionProperty l_grabAction;

    public float wrapTimer;
    public Slider wrapSlider;

    public int ingredientCount;
    public Transform ingredientHolder;

    public bool send;

    private void Start()
    {
        daytime = FindObjectOfType<Daytime>();
    }

    private void Update()
    {
        wrapSlider.value = wrapTimer;
        if (send)
        {
            send = false;
            StartCoroutine(Wrap());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Food")
        {
            if (thisShaurma.ContainsKey(other.gameObject.GetComponent<Food>().ID))
            {
                thisShaurma[other.gameObject.GetComponent<Food>().ID] += 1;
            }
            else
            {
                thisShaurma.Add(other.gameObject.GetComponent<Food>().ID, 1);
            }
            Transform t = Instantiate(daytime.allProducts[other.gameObject.GetComponent<Food>().ID].model, ingredientHolder).transform;
            t.localPosition = new Vector3(0, ingredientCount * 0.02f, 0);
            ingredientCount++;
            Destroy(other.gameObject);
            keys.Clear();
            values.Clear();
            keys = thisShaurma.Keys.ToList();
            values = thisShaurma.Values.ToList();
            wrapTimer = 0;
        }
        else if (other.tag == "RightHand")
        {
            if (r_grabAction.action.ReadValue<float>() > 0)
            {
                wrapTimer += Time.deltaTime;
                if (wrapTimer >= 0.75f)
                {
                    StartCoroutine(Wrap());
                }
            }
        }
        else if (other.tag == "LeftHand")
        {
            if (l_grabAction.action.ReadValue<float>() > 0)
            {
                wrapTimer += Time.deltaTime;
                if (wrapTimer >= 0.75f)
                {
                    StartCoroutine(Wrap());
                }
            }
        }
    }

    public IEnumerator Wrap()
    {
        lavash.SetActive(false);
        shaurma.SetActive(true);

        shaurma.transform.LookAt(daytime.shaurma_destination.position);
        shaurma.transform.Rotate(shaurma.transform.up, 90);
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        float time = 0;
        while (time < 0.75f)
        {
            time += Time.deltaTime; 
            transform.position = Vector3.Lerp(transform.position, daytime.shaurma_destination.position, time * 4 / 3);
            yield return null;
        }
        daytime.CheckShaurma();
        Destroy(gameObject);
    }
}
