using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.parent.position += Vector3.left * 2f * Time.deltaTime;
        transform.parent.Rotate(new Vector3(0, 0, 500 * Time.deltaTime));
        transform.Rotate(new Vector3(0,0, -500 * Time.deltaTime));
    }
}
