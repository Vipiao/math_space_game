using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting_sound_script : MonoBehaviour
{
    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
        AudioSource audioSource = GetComponent<AudioSource>();
        //audioSource.time = Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
