using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MashGenerate : MonoBehaviour
{
    private MeshFilter meshFilter;
    private Mesh mesh;

    public float upDownFactor = 0.1f;
    public float upDownSpeed = 6f;
    public float leftFactor = 0.3f;
    public float leftSpeed = 3f;
    public float leftOffSet = 2.3f;
    public float StrechFactor = -0.1f;
    public float StrechSpeed = 6f;


    private void Start()
    {
        mesh = new Mesh();
        mesh.name = "GenerateMesh";

        mesh.vertices = GenerateVerts();
        mesh.triangles = GenerateTries();

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

    private int[] GenerateTries()
    {
        return new int[]
        {
            1,0,2,
            2,0,3, 
            4,5,6,
            4,6,7,

            9,10,11,
            8,10,9,
            12,13,15,
            14,12,15,

            16,17,19,
            18,16,19,
            20,21,23,
            22,20,23
        };
    }

    private Vector3[] GenerateVerts(float up = 0f, float left = 0f, float stretch = 0f)
    {
        return new Vector3[]
        {
            new Vector3(-1f, 0f, 1f),
            new Vector3(1f, 0f, 1f),
            new Vector3(1f, 0f, -1f),
            new Vector3(-1f, 0f, -1f),

            new Vector3(-1f - stretch + left, 2f + up, 1f + stretch),
            new Vector3(1f + stretch + left, 2f+ up, 1f + stretch),
            new Vector3(1f  + stretch + left, 2f+ up, -1f - stretch),
            new Vector3(-1f  - stretch + left, 2f+ up, -1f - stretch),

            new Vector3(-1f, 0f, 1f),
            new Vector3(-1f, 0f, -1f),
            new Vector3(-1f - stretch + left, 2f+ up, 1f + stretch),
            new Vector3(-1f - stretch + left, 2f+ up, -1f - stretch),

            new Vector3(1f, 0f, 1f),
            new Vector3(1f, 0f, -1f),
            new Vector3(1f + stretch + left, 2f+ up, 1f + stretch),
            new Vector3(1f + stretch + left, 2f+ up, -1f - stretch),

            new Vector3(1f, 0f, -1f),
            new Vector3(-1f, 0f, -1f),
            new Vector3(1f + stretch + left, 2f+ up, -1f - stretch),
            new Vector3(-1f - stretch + left, 2f+ up, -1f - stretch),

            new Vector3(-1f, 0f, 1f),
            new Vector3(1f, 0f, 1f),
            new Vector3(-1f - stretch + left, 2f+ up, 1f + stretch),
            new Vector3(1f + stretch + left, 2f+ up, 1f + stretch)
        };
    }

    private void Update()
    {
        mesh.vertices = GenerateVerts(Mathf.Sin(Time.realtimeSinceStartup * upDownSpeed) * upDownFactor,
            Mathf.Sin(Time.realtimeSinceStartup * leftSpeed + leftOffSet) * leftFactor,
            Mathf.Sin(Time.realtimeSinceStartup * StrechSpeed) * StrechFactor);
    }
}
