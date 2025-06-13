using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using System.Collections;

public class CinematicaInicio : MonoBehaviour
{
    [SerializeField] private string nombreEscena = "Level0";

    public PlayableDirector timeline;

    void Start()
    {
        timeline.stopped += OnTimelineFinished;
        timeline.Play();
    }

    void OnTimelineFinished(PlayableDirector pd)
    {
        Debug.Log("Timeline terminó, lanzando fade out");

        //CrossfadeManager.Instance.LoadScene(nombreEscena);
    }
}
