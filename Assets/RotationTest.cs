using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Extracts the angle - axis rotation from the transform rotation

        

        //Debug.Log(axis);
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Quaternion.Angle(transform.rotation, Quaternion.identity);
        Debug.Log(transform.eulerAngles);
    }
}
