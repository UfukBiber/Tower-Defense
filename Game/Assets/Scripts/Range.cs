using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    private Vector3[] vertices;
    private int[] triangles;
    public int totalSteps;
    public float range;

    Mesh mesh;
    public MeshFilter meshFilter;
    void Start()
    { 
        mesh = new Mesh();
        meshFilter.mesh = mesh;
    }

    void Update()
    {
        DrawRange();
    }
    
    void DrawRange()
    {
        vertices = new Vector3[totalSteps + 1];
        triangles = new int[totalSteps * 3];
        vertices[0] = transform.localPosition;
        float stepAngle = 360f / totalSteps;
        int j = 1;
        for (float angle = 0; angle < 360; angle = angle + stepAngle)
        {
            Vector3 end = transform.localPosition + AngleToDirection(angle) * range;
            vertices[j] = transform.InverseTransformPoint(end);
            j++;
        }
        for (int i = 0; i < totalSteps - 1; i++)
        {
            triangles[3 * i] = 0;
            triangles[3 * i + 1] = i + 1;
            triangles[3 * i + 2] = i + 2;
        }
        triangles[totalSteps * 3 - 3] = 0;
        triangles[totalSteps * 3 - 2] = vertices.Length - 1;
        triangles[totalSteps * 3 - 1] = 1;
        Debug.Log(triangles.Length);
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    Vector3 AngleToDirection(float angle, bool isGlobal = false)
    {
        if (!isGlobal)
        {
            angle += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0f, Mathf.Cos(Mathf.Deg2Rad * angle));
    }
}
