using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStick : MonoBehaviour
{
    private Vector3 startPosition;

    // Reference points of edges of the stick
    private Vector3 leftEdge;
    private Vector3 rightEdge;

    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float rotateSpeed = 10.0f;

    private float maxRotate = 100f;
    private float minRotate = 80f;

    private float upperBound = 1.85f;

    public void Start()
    {
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // Direction booleans
        bool isLeftUp = Input.GetKey(KeyCode.W);
        bool isLeftDown = Input.GetKey(KeyCode.S);
        bool isRightUp = Input.GetKey(KeyCode.O);
        bool isRightDown = Input.GetKey(KeyCode.L);

        Vector3 offset = transform.up * (transform.localScale.y);
        leftEdge = transform.position + offset;
        rightEdge = transform.position - offset;

        // Limit of rotation of the stick
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, 0f, 0f);
        transform.position = pos;

        if (isLeftUp && isRightUp)
        {
            if (!((transform.GetChild(0).position.y) > upperBound) && !((transform.GetChild(1).position.y) > upperBound))
            {
                MoveUp();
            }
        }
        else if (isLeftUp && !isRightUp)
        {
            if (!((transform.GetChild(1).position.y) > upperBound))
            {
                MoveLefttSideUp();
            }
        }
        else if (isRightUp && !isLeftUp)
        {
            if (!((transform.GetChild(0).position.y) > upperBound))
            {
                MoveRightSideUp();
            }
        }

        if (isLeftDown && isRightDown)
        {
            if (!((transform.GetChild(0).position.y) < startPosition.y) && !((transform.GetChild(1).position.y) < startPosition.y))
            {
                MoveDown();
            }
        }
        else if (isLeftDown && !isRightDown)
        {
            if (!((transform.GetChild(1).position.y) < startPosition.y))
            {
                MoveLeftSideDown();
            }
        }
        else if (isRightDown && !isLeftDown)
        {
            if (!((transform.GetChild(0).position.y) < startPosition.y))
            {
                MoveRightSideDown();
            }
        }
    }

    private void MoveUp()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.World);
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.World);
    }

    private void MoveRightSideUp()
    {
        float angleZ = transform.rotation.eulerAngles.z;
        angleZ = Mathf.Clamp(angleZ, minRotate, maxRotate);
        if (angleZ != maxRotate)
        {
            angleZ += rotateSpeed * Time.deltaTime;
            transform.RotateAround(leftEdge, Vector3.forward, rotateSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0f, 0f, angleZ);
        }
    }

    private void MoveRightSideDown()
    {
        float angleZ = transform.rotation.eulerAngles.z;
        angleZ = Mathf.Clamp(angleZ, minRotate, maxRotate);
        if (angleZ != minRotate)
        {
            angleZ -= rotateSpeed * Time.deltaTime;
            transform.RotateAround(leftEdge, Vector3.back, rotateSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0f, 0f, angleZ);
        }
    }

    private void MoveLefttSideUp()
    {
        float angleZ = transform.rotation.eulerAngles.z;
        angleZ = Mathf.Clamp(angleZ, minRotate, maxRotate);
        if (angleZ != minRotate)
        {
            angleZ -= rotateSpeed * Time.deltaTime;
            transform.RotateAround(rightEdge, Vector3.back, rotateSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0f, 0f, angleZ);

        }
    }

    private void MoveLeftSideDown()
    {
        float angleZ = transform.rotation.eulerAngles.z;
        angleZ = Mathf.Clamp(angleZ, minRotate, maxRotate);
        if (angleZ != maxRotate)
        {
            angleZ += rotateSpeed * Time.deltaTime;
            transform.RotateAround(rightEdge, Vector3.forward, rotateSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0f, 0f, angleZ);

        }
    }
}
