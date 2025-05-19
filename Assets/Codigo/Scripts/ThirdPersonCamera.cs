using System.Collections;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 5, -10);
    private Vector3 offsetOriginal;
    public Vector3 offsetZoom = new Vector3(0, 3, -6);
    private Transform target;

    [Range(0, 1)] public float lerpValue = 0.1f;

    private Coroutine zoomCoroutine;

    void Start()
    {
        target = FindFirstObjectByType<PlayerController>().transform;
        offsetOriginal = offset;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpValue);
    }

    public void HacerZoomTemporal(float duracion = 2f)
    {
        if (zoomCoroutine != null)
            StopCoroutine(zoomCoroutine);

        zoomCoroutine = StartCoroutine(ZoomTemporal(duracion));
    }

    private IEnumerator ZoomTemporal(float duracion)
    {
        offset = offsetZoom;
        yield return new WaitForSeconds(duracion);
        offset = offsetOriginal;
    }
}
