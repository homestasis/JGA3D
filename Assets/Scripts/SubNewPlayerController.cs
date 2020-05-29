using UnityEngine;

public partial class NewPlayerController : MonoBehaviour
{
    [SerializeField] private GameObject weatherOb;
    private WeatherController weather;
    private bool isStop;
    private Vector3 StopPoint;


    internal bool IntoSight()
    {
        if (weather.GetIsHeavyRainy()) { return false; }

        return true;
    }

    internal void StopPlayer()
    {
        isStop = true;
        isRun = false;
        StopPoint = transform.position;
    }

    internal void ResetIsStop()
    {
        isStop = false;
    }


   
}
