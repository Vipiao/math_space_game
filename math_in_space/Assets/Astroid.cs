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

    private void Update()
    {
        var collision = AstroidObject.GetComponent<Collision>();
        HasBeenHit = collision.HasBeenHit;
    }

}
