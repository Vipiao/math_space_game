using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quad_resize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 lowerLeft, upperRight;
        Utilities.getBoundaries(out lowerLeft, out upperRight);
        float size = (upperRight.x - lowerLeft.x)/27.0f;
        transform.localScale = new Vector3(
            transform.localScale.x, transform.localScale.y, transform.localScale.z
            ) * size;
        //Debug.Log("size " + size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
