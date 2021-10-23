using UnityEngine;

public class Collision : MonoBehaviour
{
    // Start is called before the first frame update
    public bool HasBeenHit = false;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            HasBeenHit = true;
            Object.Destroy(other.gameObject);
        }
    }
}
