using UnityEngine;

public class ALU : MonoBehaviour
{
    public int health = 5;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PackageToALU"))
        {
            Package packageObject = other.GetComponent<Package>();
                Debug.Log(packageObject);
            if (packageObject.hacked) {
                Debug.Log("hacked");
                health--;
            }
            Destroy(other.gameObject); // Package disappears at ALU
        }
    }
}
