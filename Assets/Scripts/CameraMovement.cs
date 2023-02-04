using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject _playerCameraOffset = null;
    [SerializeField] private float _smoothingFactor = 10f;

    private void Awake()
    {
        if (_playerCameraOffset == null)
        {
            Debug.LogError("MISSING PLAYER CAMERA OFFSET, ADD REFERENCE FROM PLAYER");
        }
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _playerCameraOffset.transform.position, Time.deltaTime * _smoothingFactor);
    }
}
