using UnityEngine;

public class ParentNull : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("stair"))
        {
            transform.parent = null;
            
            GetComponent<Rigidbody>().isKinematic = false;
            
            GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
