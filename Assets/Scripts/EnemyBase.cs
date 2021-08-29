using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    public float maxSpeed;

    [HideInInspector]
    [System.NonSerialized]
    public Vector3 targetDir;

    [HideInInspector]
    [System.NonSerialized]
    public bool canMove = true;

    private Animator animator;
    public Animator Animator { get => animator; private set => animator = value; }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var rb = GetComponent<Rigidbody2D>();

        //rb.AddForce(targetDir);


        //rb.velocity = Vector3.MoveTowards(rb.velocity, targetDir * targetSpeed, Time.deltaTime * acceleration);

        rb.velocity = rb.velocity.normalized * Mathf.Clamp(rb.velocity.magnitude, 0f, maxSpeed);

        bool isWalking = Mathf.Abs(rb.velocity.x) > 0.3f;

        if (isWalking)
        {
            var scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(-rb.velocity.x);
            transform.localScale = scale;
        }

        Animator.SetBool("isWalking", isWalking);
    }
}
