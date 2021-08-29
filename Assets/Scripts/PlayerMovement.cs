using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    Vector2 movement;
    Gate beingOpened;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        if (Mathf.Abs(movement.x) > 0.01f)
        {
            var scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(movement.x);
            transform.localScale = scale;
        }

        if (beingOpened)
        {
            float progress = beingOpened.Open(Time.deltaTime);

            if (progress >= 1f)
            {
                beingOpened = null;
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        //transform.up = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var gate = collision.collider.gameObject.GetComponent<Gate>();

        if (gate)
        {
            beingOpened = gate;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (beingOpened && collision.collider.gameObject == beingOpened.gameObject)
        {
            beingOpened = null;
        }
    }
}
