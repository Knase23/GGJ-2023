using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovingBox : MonoBehaviour
{
    public List<Transform> targets;

    public bool reachEndGoBack;
    private int targetIndex;
    private int inverse = 1;
    private float lerpTime;
    private Vector2 lerpStartPos;

    public float timeToNextTarget = 1;


    public Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        if (targets.Count == 0)
        {
            Debug.LogWarning("No Targets for MovingBox to go to", gameObject);
            Destroy(this);
            return;
        }

        var a = new GameObject("StartPoint for a MovingBox");
        a.transform.position = transform.position;
        targets.Add(a.transform);


        targetIndex = 0;
        lerpStartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        lerpTime += Time.deltaTime;
        if (targetIndex < 0)
            targetIndex = 0;

        Vector2 distanceToTravel = (Vector2)targets[targetIndex].position - lerpStartPos;

        velocity = distanceToTravel / timeToNextTarget;
        
        transform.position = lerpStartPos + velocity * lerpTime;
        if (lerpTime < timeToNextTarget) return;

        lerpTime = 0;
        lerpStartPos = targets[targetIndex].position;
        targetIndex += 1 * inverse;
        if (inverse > 0 && targetIndex >= targets.Count)
        {
            if (reachEndGoBack)
            {
                inverse *= -1;
                if (targetIndex >= targets.Count)
                    targetIndex = targets.Count-1;
                targetIndex += 1 * inverse;
            }
            else
            {
                inverse = 1;
                targetIndex = 0;
            }
        }
        else if (inverse < 0 && targetIndex < 0)
        {
            inverse *= -1;
            if (targetIndex < 0)
                targetIndex = 0;
            targetIndex += 1 * inverse;
        }
    }
}