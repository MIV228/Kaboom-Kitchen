using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

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
    public Night nightController;

    [Header("References")]
    public Transform twohandSpawn;
    public Transform playerSpawn;

    public GameObject xr_twohanded;
    public GameObject xr_player;

    [Header("Time")]
    [SerializeField] public Time currTime;
    public int currDay;

    public GameObject sun;
    public GameObject even;
    public GameObject moon;

    public TMP_Text dayText;

    public void Start()
    {
        StartDay();
    }

    public void StartDay()
    {
        currDay++;
        xr_twohanded.SetActive(true);
        xr_player.SetActive(false);
        xr_twohanded.transform.position = twohandSpawn.position;
        moon.SetActive(false);
        sun.SetActive(true);
        currTime = Time.Day;
        dayController.StartDay();
        dayText.text = "Δενό " + currDay.ToString();
    }

    public void StartEvening()
    {

    }

    public void StartNight()
    {
        xr_player.SetActive(true);
        xr_twohanded.SetActive(false);
        xr_player.transform.position = playerSpawn.position;
        currTime = Time.Night;
        sun.SetActive(false);
        moon.SetActive(true);
        nightController.StartNight();
    }
}
