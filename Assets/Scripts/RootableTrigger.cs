using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootableTrigger : MonoBehaviour
{
    private bool _isSnappedTo = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (Player.Instance.Jump.IsGrounded || _isSnappedTo) return;
            Debug.Log("player entered trigger");
            //Vector3 castDirection = other.transform.position - transform.position;

            //if (castDirection.x >= 0)
            //{
            //    castDirection = Vector3.right;
            //}
            //else
            //{
            //    castDirection = Vector3.left;
            //}
            Debug.DrawRay(other.transform.position, transform.position - other.transform.position, Color.red, 0.4f);


            Vector3 directionToOther = transform.position - other.transform.position;
            if (Vector3.Dot(directionToOther, Vector3.right) > 0)
            {
                directionToOther = Vector3.right;
            }
            else
            {
                directionToOther = Vector3.left;
            }
            Debug.DrawRay(other.transform.position, directionToOther, Color.magenta, 0.4f);
            
            if (Physics.Raycast(other.transform.position, directionToOther, out RaycastHit hit, 1.5f))
            {
                Debug.Log("raycast to wall hit");
                other.GetComponent<PlayerRooting>().RootToSurface(hit.point, hit.normal);
                _isSnappedTo = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isSnappedTo = false;
    }
}
