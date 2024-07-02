using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carInput : MonoBehaviour
{
    carController carController;

    void Awake()
    {
        carController = GetComponent<carController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        carController.SetInputVector(inputVector);
    }
}
