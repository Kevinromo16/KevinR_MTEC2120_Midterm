using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{

    public float speed = 1; 

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(5, 8);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);
    }

}
