using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SectionCreated : MonoBehaviour
{
    public float yDistanceBetweenSections;

    public int numberOfSections;

    public GameObject sectionPrefab;

    public GameObject playerStartingPositionChanger;
    public void Awake()
    {
        for (int i = 0; i < numberOfSections; i++)
        {
            Instantiate(sectionPrefab, Vector3.up * i * yDistanceBetweenSections, quaternion.identity, transform);
        }

        playerStartingPositionChanger.transform.position = Vector3.up * (numberOfSections + 1) * yDistanceBetweenSections;
    }
}
