using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMover : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    // Start is called before the first frame update


    // when spawned-- stars will hop!
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
