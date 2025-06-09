using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CinematicaInicio : MonoBehaviour
{
    [SerializeField] private string nombreEscena = "Level0";
    public PlayableDirector timeline;
    public PlayerController playerController; 

    void Start()
    {
       
        if (playerController != null)
        {
            playerController.puedeMover = false;
        }

        timeline.stopped += OnTimelineFinished;
        timeline.Play();
    }

    void OnTimelineFinished(PlayableDirector pd)
    {
        Debug.Log("Timeline terminó, lanzando fade out");

        if (playerController != null)
            playerController.enabled = false; 

        CrossfadeManager.Instance.LoadScene(nombreEscena);
    }

}
