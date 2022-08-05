using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float movementSpeed = 10f; 
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float magnitute = Mathf.Max(Mathf.Abs(horizontalInput), Mathf.Abs(verticalInput));
        transform.position += new Vector3(horizontalInput, verticalInput, 0).normalized * magnitute * movementSpeed * Time.deltaTime;
    }
}
