using UnityEngine;

public class movement : MonoBehaviour
{
    public Transform duck;
    public float speed = 0.125f;
    public Vector3 offset;

     void LateUpdate()
    {
        Vector3 desiredPosition = duck.position + offset;
        transform.position = desiredPosition;
    }
}
