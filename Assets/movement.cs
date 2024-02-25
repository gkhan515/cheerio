using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject rock;

    //keyboard movement
    private Vector2 movementDirection;
    [SerializeField] private float movementSpeed = 2f;

    //grapple movement
    [SerializeField] private float forceAmount = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D click = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (click.collider != null && click.collider.gameObject.name == "rock")
            {
                //Debug.Log("Rock was clicked");
                Vector2 direction = (rock.transform.position - rb.transform.position).normalized;
                //Debug.Log(direction);
                rb.AddForce(direction * forceAmount);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection.normalized * movementSpeed;
    }
}
