using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    [SerializeField] public Time currTime;
    public int currDay;

    public void Start()
    {
        StartDay();
    }

    public void StartDay()
    {
        dayController.StartDay();
    }

    public void StartEvening()
    {

    }

    public void StartNight()
    {
        nightController.StartNight();
    }
}
