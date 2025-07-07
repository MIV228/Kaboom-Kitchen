using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameController : MonoBehaviour
{
    [System.Serializable]
    public enum Time
    {
        Day,
        Evening,
        Night
    }

    [Header("Scripts")]
    public Daytime dayController;
    public Shop shop;
    public Night nightController;

    [Header("References")]
    public Transform twohandSpawn;
    public Transform shopSpawn;
    public Transform playerSpawn;

    public GameObject xr_twohanded;

    public Jump jump;
    public Player player;
    public GameObject knife;
    public GameObject rightHand;

    public Transform r_FoodHolder;
    public Transform l_FoodHolder;

    [Header("Time")]
    [SerializeField] public Time currTime;
    public int currDay;

    public GameObject sun;
    public GameObject even;
    public GameObject moon;

    public AudioSource music_day;
    public AudioSource music_shop;
    public AudioSource music_night;

    public TMP_Text dayText;

    public void Start()
    {
        r_FoodHolder = GameObject.Find("r_FoodHolder").transform;
        l_FoodHolder = GameObject.Find("l_FoodHolder").transform;
        StartDay();
    }

    public void StartDay()
    {
        currDay++;
        jump.enabled = false;
        player.enabled = false;
        music_day.Play();
        music_night.Stop();
        knife.SetActive(false);
        rightHand.SetActive(true);
        xr_twohanded.transform.position = twohandSpawn.position;
        moon.SetActive(false);
        sun.SetActive(true);
        currTime = Time.Day;
        dayController.StartDay();
        dayText.text = "Δενό " + currDay.ToString();
    }

    public void StartEvening()
    {
        currTime = Time.Evening;
        jump.enabled = true;
        player.enabled = true;
        music_shop.Play();
        shop.Regenerate();
        xr_twohanded.transform.position = shopSpawn.position;
    }

    public void StartNight()
    {
        music_night.Play();
        music_shop.Stop();
        knife.SetActive(true);
        rightHand.SetActive(false);
        foreach (Transform t in r_FoodHolder)
        {
            if (t != r_FoodHolder) Destroy(t.gameObject);
        }
        foreach (Transform t in l_FoodHolder)
        {
            if (t != l_FoodHolder) Destroy(t.gameObject);
        }
        xr_twohanded.transform.position = playerSpawn.position;
        currTime = Time.Night;
        sun.SetActive(false);
        moon.SetActive(true);
        nightController.StartNight();
    }
}
