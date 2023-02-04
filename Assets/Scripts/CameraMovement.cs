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
        //var targetPosition = new Vector3(transform.position.x, _playerCameraOffset.transform.position.y, transform.position.z);
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _smoothingFactor);
    }

    private void FixedUpdate()
    {

        float targetXPos = transform.position.x;
        var playerOffsetX = _playerCameraOffset.transform.position.x;
        if (playerOffsetX > transform.position.x + 5 || playerOffsetX < transform.position.x -5)
        {
            targetXPos = playerOffsetX;
        }
        
        var targetPosition = new Vector3(targetXPos, _playerCameraOffset.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * _smoothingFactor);
    }
}
