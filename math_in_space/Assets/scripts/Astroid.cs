using TMPro;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshPro TextBox;

    public GameObject AstroidObject;

    public bool IsScoreObject = false;

    public bool HasBeenHit = false;

    public int value;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Bolt")
        {
            HasBeenHit = true;
            Destroy(collision.gameObject);
        }
    }

    private void Update()
    {
        if (tag != "Player")
        {
            var collision = AstroidObject.GetComponent<Collision>();
            HasBeenHit = collision.HasBeenHit;
        }

        /*transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(transform.position.x, transform.position.y, -10.0f),
            0.001f);*/
        //Rigidbody rigidbody = this.gameObject.transform.GetChild(0).GetComponent<Rigidbody>();
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        float gravity = 0.1f;
        rigidbody.velocity += -Time.deltaTime * rigidbody.position * gravity;

        // Center asteroid.
        //Vector3 displacement = rigidbody.transform.position;
        //this.gameObject.transform.position += displacement;
        //rigidbody.transform.position -= displacement;
        //this.gameObject.transform.GetChild(1).transform.rotation =
        //    Quaternion.Inverse(rigidbody.transform.rotation);
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
        rigidbody.position = new Vector3(rigidbody.position.x, 0, rigidbody.position.z);
    }

}
