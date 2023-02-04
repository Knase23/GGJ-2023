using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotator : MonoBehaviour
{

    [SerializeField] private Vector3 _direction = Vector3.one;
    void Update()
    {
        transform.Rotate(_direction * Time.deltaTime);
    }
}
