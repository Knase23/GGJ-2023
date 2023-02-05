using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinFloat : MonoBehaviour
{
   float timer;
   public float speed;
   public float magnitude;
   float sin;
   Vector3 startPos;

   void Start()
   {
      timer = Random.value * 127.23423f;
      startPos = transform.position;
   }

   void Update()
   {
      timer += Time.deltaTime * speed;
      sin = Mathf.Sin(timer);
      transform.position = startPos;
      transform.position += Vector3.up * magnitude * sin;
   }
}
