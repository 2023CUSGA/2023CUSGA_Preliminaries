using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position += new Vector3(-10 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position += new Vector3(10 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.position += new Vector3(0, 10 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.position += new Vector3(0, -10 * Time.deltaTime, 0);
        }
    }










}
