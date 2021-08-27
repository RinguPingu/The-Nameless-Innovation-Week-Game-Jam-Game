using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    public float maxSpeed;

    [HideInInspector]
    public Vector3 targetDir;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rb = GetComponent<Rigidbody2D>();

        //rb.AddForce(targetDir);


        //rb.velocity = Vector3.MoveTowards(rb.velocity, targetDir * targetSpeed, Time.deltaTime * acceleration);

        rb.velocity = rb.velocity.normalized * Mathf.Clamp(rb.velocity.magnitude, 0f, maxSpeed);
    }
}
