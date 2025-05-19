using UnityEngine;
using UnityEngine.Playables;

public class CinematicaInicio : MonoBehaviour
{
    public PlayableDirector timeline;

    void Start()
    {
        timeline.stopped += OnTimelineFinished;
        timeline.Play();
    }

    void OnTimelineFinished(PlayableDirector pd)
    {
        Debug.Log("Timeline terminó, lanzando fade out");
        FindObjectOfType<FadeManager>().StartFadeOut();
    }
}
