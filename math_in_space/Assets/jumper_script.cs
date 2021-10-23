using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumper_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.position = new Vector3(0,1,-20);
        rigidbody.velocity = new Vector3(0,0,24);

        Destroy(rigidbody.gameObject, 5);

        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        int randInt = Random.Range(0, 3);
        this.gameObject.transform.GetChild(randInt).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        float gravity = 10;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity += Time.deltaTime * new Vector3(0,0,-1) * gravity;
        rigidbody.rotation = Quaternion.Euler(90, 0, 0) *
            Quaternion.Euler(0, 0, Mathf.Cos(Time.time*2)*50);
    }
}
