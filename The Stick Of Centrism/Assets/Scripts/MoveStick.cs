using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStick : MonoBehaviour
{
    private Vector3 startPosition = new Vector3(0f, 0f, 0f);
    private Vector3 leftEdge;
    private Vector3 rightEdge;

    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float rotateSpeed = 10.0f;

    private float maxRotate = 100f;
    private float minRotate = 80f;

    private void Start()
    {
        transform.position = startPosition;
    }

    private void Update()
    {
        Vector3 offset = transform.up * (transform.localScale.y);
        leftEdge = transform.position + offset;
        rightEdge = transform.position - offset;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, 0f, 0f);
        transform.position = pos;

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.O))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.O))
        {
            float angleZ = transform.rotation.eulerAngles.z;
            angleZ -= rotateSpeed * Time.deltaTime;
            angleZ = Mathf.Clamp(angleZ, minRotate, maxRotate);
            transform.RotateAround(rightEdge, Vector3.back, rotateSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0f, 0f, angleZ);
        }
        else if (Input.GetKey(KeyCode.O) && !Input.GetKey(KeyCode.W))
        {
            float angleZ = transform.rotation.eulerAngles.z;
            angleZ += rotateSpeed * Time.deltaTime;
            angleZ = Mathf.Clamp(angleZ, minRotate, maxRotate);
            transform.RotateAround(leftEdge, Vector3.forward, rotateSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0f, 0f, angleZ);
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.L))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.L))
        {
            float angleZ = transform.rotation.eulerAngles.z;
            angleZ += rotateSpeed * Time.deltaTime;
            angleZ = Mathf.Clamp(angleZ, minRotate, maxRotate);
            transform.RotateAround(rightEdge, Vector3.forward, rotateSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0f, 0f, angleZ);
        }
        else if (Input.GetKey(KeyCode.L) && !Input.GetKey(KeyCode.S))
        {
            float angleZ = transform.rotation.eulerAngles.z;
            angleZ -= rotateSpeed * Time.deltaTime;
            angleZ = Mathf.Clamp(angleZ, minRotate, maxRotate);
            transform.RotateAround(leftEdge, Vector3.back, rotateSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0f, 0f, angleZ);
        }
    }
}
