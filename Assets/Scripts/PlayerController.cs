using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static Vector2 recentMovement;
    public Rigidbody rb;
    private float movementX;
    private float movementY;
    private bool jumpPressed;

    public float speed;

    public TextMeshProUGUI countText;
    public TextMeshProUGUI winText;
    private int count = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpPressed = false;
        recentMovement = new Vector2(0.0f, 0.0f);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movement = movementValue.Get<Vector2>();

        Vector3 velocity3 = rb.velocity.normalized;
        Vector2 velocity2 = new Vector2(velocity3.x, velocity3.z);

        float yRotation = Mathf.Atan2(velocity2.x, velocity2.y) * Mathf.Rad2Deg;
        //Debug.Log("yRotation" + yRotation.ToString());

        if (Mathf.Abs(yRotation) >= 90)
        {
            movementX = - movement.x;
            movementY = - movement.y;
        } 
        else
        {
            movementX = movement.x;
            movementY = movement.y;
        }

        recentMovement = movement;
    }

    private void OnJump()
    {
        jumpPressed = true;
        Debug.Log("OnJump");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float movementZ = 0.0f;

        if (jumpPressed && rb.velocity.y == 0.0f)
        {
            movementZ = speed;
            jumpPressed = false;
            Debug.Log("FixedUpdate");
        }

        Vector3 movement = new Vector3(movementX, movementZ * 10.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();

            if(count >= 12)
            {
                winText.gameObject.SetActive(true);
            }
        }
    }

    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
}
