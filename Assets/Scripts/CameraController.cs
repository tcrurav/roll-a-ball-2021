using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private float angleOffset;
    private float lastRotation;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        angleOffset = Mathf.Atan2(offset.x, offset.y) * Mathf.Rad2Deg;

        lastRotation = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 velocity3 = player.GetComponent<Rigidbody>().velocity.normalized;
        Vector2 velocity2 = new Vector2(velocity3.x, velocity3.z);

        float yRotation = Mathf.Atan2(velocity2.x, velocity2.y) * Mathf.Rad2Deg;

        float angleToApply = angleOffset - yRotation;
        Vector2 counterOffset = RotateVector(new Vector2(offset.x, offset.z), angleToApply);

        transform.position = player.transform.position + new Vector3(counterOffset.x, offset.y, counterOffset.y);
        
        float rotationToApply = yRotation - lastRotation;

        lastRotation = yRotation;
        transform.Rotate(new Vector3(0.0f, rotationToApply, 0.0f));
    }

    public Vector2 RotateVector(Vector2 v, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float _x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
        float _y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);
        return new Vector2(_x, _y);
    }
}
