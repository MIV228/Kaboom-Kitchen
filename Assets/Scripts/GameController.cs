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

    [SerializeField] public Time currTime;
    public int currDay;

    public void Start()
    {
        currDay = 1;
    }
}
