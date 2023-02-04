using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootableTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
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
            if (Physics.Raycast(other.transform.position, transform.position - other.transform.position, out RaycastHit hit, Mathf.Infinity))
            {
                Debug.Log("raycast to wall hit");
                other.GetComponent<PlayerRooting>().RootToSurface(hit.point, hit.normal);
            }
        }
    }
}
