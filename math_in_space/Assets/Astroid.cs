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

    private void Update()
    {
        if (tag != "Player")
        {
            var collision = AstroidObject.GetComponent<Collision>();
            HasBeenHit = collision.HasBeenHit;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(transform.position.x, transform.position.y, -10.0f),
            0.001f);
    }

}
