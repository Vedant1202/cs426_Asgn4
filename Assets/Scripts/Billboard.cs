using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera copCamera; // Assign the Cop Camera in the Inspector
    public Transform copTransform; // Assign the Cop’s Transform in the Inspector
    public float rotationSpeed = 2f; // Adjust for smooth rotation
    public float updateInterval = 2f; // Updates every 2 seconds

    void Start()
    {
        if (copCamera == null)
        {
            GameObject copCamObject = GameObject.FindWithTag("CopCamera");
            if (copCamObject != null)
                copCamera = copCamObject.GetComponent<Camera>();
        }

        if (copTransform == null)
        {
            GameObject copObject = GameObject.FindWithTag("Cop");
            if (copObject != null)
                copTransform = copObject.transform;
        }

        InvokeRepeating(nameof(UpdateRotation), 0f, updateInterval);
    }

    void UpdateRotation()
    {
        if (copCamera != null)
        {
            // Rotate toward the Cop Camera
            StartCoroutine(SmoothRotate(copCamera.transform.position));
        }
        else if (copTransform != null)
        {
            // If no camera, rotate toward the Cop itself
            StartCoroutine(SmoothRotate(copTransform.position));
        }
    }

    System.Collections.IEnumerator SmoothRotate(Vector3 targetPosition)
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - targetPosition);
        float elapsedTime = 0f;
        float duration = 1f; // Rotation duration

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}
