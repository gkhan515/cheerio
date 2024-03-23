using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject rock;

    [SerializeField] private float frictionCoefficient = 2f;

    //keyboard movement
    private Vector2 keyboardDirection;
    private float keyboardStrength = 1f;
    private Vector2 keyboardForce;
    [SerializeField] private float keyboardAccelerationScalar = 1f;

    //grapple movement
    private RaycastHit2D click;
    private bool currentlyClicking;
    private Vector2 mousePosition;
    private Vector2 grappleDirection;
    [SerializeField] private float grappleForce = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        keyboardDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        currentlyClicking = Input.GetMouseButton(0);
    }

    private void FixedUpdate()
    {
        keyboardStrength = rb.velocity.magnitude * keyboardAccelerationScalar + 1;

        if (keyboardDirection == Vector2.zero) 
        {
            rb.velocity -= rb.velocity * frictionCoefficient * Time.deltaTime;
        }
        else
        {
            Debug.Log("clicking button");
            keyboardForce = keyboardDirection * keyboardStrength;
            rb.AddForce(keyboardForce);
        }
        if (currentlyClicking)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            click = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (click.collider != null && click.collider.gameObject.name == "rock")
            {
                grappleDirection = (rock.transform.position - rb.transform.position).normalized;
                rb.AddForce(grappleDirection * grappleForce);
            }
        }
    }
}
