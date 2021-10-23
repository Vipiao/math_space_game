using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dead_face : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 4);
        float speed = 10;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(
            Random.Range(-speed, speed),
            0,
            Random.Range(-speed, speed)
            );
        rigidbody.rotation = Quaternion.Euler(90,0,0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
