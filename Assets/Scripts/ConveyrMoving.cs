using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyrMoving : MonoBehaviour
{
    [SerializeField] private float endPosX;
    [SerializeField] private float speed;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        if (transform.position.x >= endPosX)
        {

            transform.Translate(Vector3.left * speed * Time.deltaTime);
            return;
        }

        transform.position = startPos;
    }
}
