using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShoot : MonoBehaviour
{

    public GameObject s;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject r = Instantiate(s, this.transform.position, this.transform.rotation);
            r.GetComponent<Rigidbody>().AddForce(this.transform.forward * 10, ForceMode.Impulse);
        }
    }
}
