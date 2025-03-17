using UnityEngine;

public class CopLight : MonoBehaviour
{
    public Light policeLight; // Assign a Point Light in the Inspector
    public float flashSpeed = 1f; // Adjust speed of flashing

    private bool isRed = true;

    void Start()
    {
        if (policeLight == null)
        {
            policeLight = GetComponent<Light>(); // Auto-assign if not set
        }

        InvokeRepeating("ToggleLight", 0f, flashSpeed); // Toggle every 'flashSpeed' seconds
    }

    void ToggleLight()
    {
        if (isRed)
        {
            policeLight.color = Color.blue; // Switch to Blue
        }
        else
        {
            policeLight.color = Color.red; // Switch to Red
        }

        isRed = !isRed; // Alternate color
    }
}
