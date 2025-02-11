using UnityEngine;

public class ALU : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PackageToALU"))
        {
            Destroy(other.gameObject); // Package disappears at ALU
        }
    }
}
