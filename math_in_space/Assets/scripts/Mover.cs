using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    float spawnTime;
    float dieTime;
    float liveTime = 10.0f;

    private void Start()
    {
        spawnTime = Time.time;
        dieTime = spawnTime + liveTime;
    }
    private void FixedUpdate()
    {
        float scale = (dieTime - Time.time) / liveTime;
        if(scale <= 0.0f)
        {
            Destroy(this.gameObject);
        }
        this.gameObject.transform.GetChild(0).transform.localScale =
            new Vector3(scale, scale, scale);
    }
}
