using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    private Mesh mesh;

    private Vector3[] vertices;
    private Vector2[] uvs;

    private int[] triangles;

    private void OnEnable()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        GenerateHexagonMesh();
        UpdateMesh();
    }

    private void GenerateHexagonMesh()
    {
        vertices = new Vector3[7];
        vertices[0] = new Vector3();
        for(int i = 1; i <= 6; i++)
        {
            vertices[i] = new Vector3(Mathf.Cos(Mathf.Deg2Rad * (30 + (i * 60))), 0, Mathf.Sin(Mathf.Deg2Rad * (30 + (i * 60))));
        }

        triangles = new int[]
        {
            0, 2, 1,
            0, 3, 2,
            0, 4, 3,
            0, 5, 4,
            0, 6, 5,
            0, 1, 6
        };
        
        uvs = new Vector2[vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2((vertices[i].x + 1) / 2, (vertices[i].z + 1) / 2);
        }
    }

    private void GenerateSquareMesh()
    {
        vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(1, 0, 1)
        };

        triangles = new int[]
        {
            0, 2, 1,
            2, 3, 1
        };

        uvs = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };

    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.uv = uvs;

        mesh.RecalculateNormals();
    }
}
