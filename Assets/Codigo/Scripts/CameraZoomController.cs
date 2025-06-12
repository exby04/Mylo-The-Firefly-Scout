using UnityEngine;
using System.Collections;

public class CameraZoomController : MonoBehaviour
{
    public static CameraZoomController instance;

    public Camera cam;

    [Header("Zoom Settings")]
    public float normalFOV = 60f;
    public float zoomFOV = 30f;
    public float zoomDuration = 0.5f;
    public float holdTime = 2f;
    public float tiltAngle = -10f;

    [Header("Pause Settings")]
    public float pausaJuegoDuracion = 2f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void ZoomOnPlayer()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomSequence_Unscaled());
    }

    public void ZoomAndPauseWithAnimation(Transform player, Animator playerAnimator)
    {
        StartCoroutine(ZoomAndPauseRoutine(player, playerAnimator));
    }

    IEnumerator ZoomAndPauseRoutine(Transform player, Animator playerAnimator)
    {
        
        if (playerAnimator != null)
        {
            playerAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
            playerAnimator.SetTrigger("RecogerObjeto");
        }

        
        Time.timeScale = 0f;

        
        var controller = player.GetComponent<PlayerController>();
        if (controller != null)
            controller.puedeMover = false;

        
        Vector3 camPosition = cam.transform.position;
        Vector3 playerPosition = player.position;
        Vector3 directionToCamera = camPosition - playerPosition;
        directionToCamera.y = 0;

        if (directionToCamera.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
            player.rotation = targetRotation;
        }

        
        yield return StartCoroutine(ZoomSequence_Unscaled());

        
        yield return new WaitForSecondsRealtime(pausaJuegoDuracion);

        
        Time.timeScale = 1f;
        if (controller != null)
            controller.puedeMover = true;
    }

    IEnumerator ZoomSequence_Unscaled()
    {
        originalPosition = cam.transform.position;
        originalRotation = cam.transform.rotation;

        Quaternion targetRotation = Quaternion.Euler(
            cam.transform.eulerAngles.x + tiltAngle,
            cam.transform.eulerAngles.y,
            cam.transform.eulerAngles.z
        );

        float timer = 0f;

        
        while (timer < zoomDuration)
        {
            float t = timer / zoomDuration;
            float eased = Mathf.SmoothStep(0, 1, t);

            cam.fieldOfView = Mathf.Lerp(normalFOV, zoomFOV, eased);
            cam.transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, eased);

            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        cam.fieldOfView = zoomFOV;
        cam.transform.rotation = targetRotation;

        
        yield return new WaitForSecondsRealtime(holdTime);

        timer = 0f;

        
        while (timer < zoomDuration)
        {
            float t = timer / zoomDuration;
            float eased = Mathf.SmoothStep(0, 1, t);

            cam.fieldOfView = Mathf.Lerp(zoomFOV, normalFOV, eased);
            cam.transform.rotation = Quaternion.Slerp(targetRotation, originalRotation, eased);

            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        cam.fieldOfView = normalFOV;
        cam.transform.rotation = originalRotation;
    }
}
