using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public int width;
    public int depth;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        mesh = GetComponent<MeshFilter>().mesh;

        GenMesh();
        UpdateMesh();
    }

    void GenMesh()
    {
        // -------------------------- create verticies --------------------------- \\
        vertices = new Vector3[(width + 1) * (depth + 1)];

        int i = 0;

        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                vertices[i] = new Vector3(x, 0, z);
               // Debug.Log(i);
                i++;
            }

        }

        // --------------------------- create triangles ------------------------------- \\

        // create triangles
        triangles = new int[width * depth * 6];
        int activeVertex = 0;

        for (int x = 0; x < width * depth - 1; x++)
        {
            // triangle 1
            triangles[activeVertex] = x; // a
            activeVertex++;
            triangles[activeVertex] = x + width; // beecause this vertex is on another row we add the width to get it // b
            activeVertex++;
            triangles[activeVertex] = x + width + 1; // c
            activeVertex++;

            // triangle 2
            triangles[activeVertex] = x;
            activeVertex++;
            triangles[activeVertex] = x + width + 1; // d
            activeVertex++;
            triangles[activeVertex] = x + 1;
            activeVertex++;

            // a = x + ((width + 1) * z)
            // b = a + 1
            // c = a + width + 1
            // d = b = width + 1

            // vertex loop breaks at the very end causing the next vertex to be all the way at "0"
            // need to set up a way to prevent the far edges from creating a triangle
        }

        //TEST              Good ol' Fashioned hard-coding
/*        vertices = new Vector3[]
        {
                    new Vector3 (0,0,0),
                    new Vector3 (0,0,1),
                    new Vector3 (1,0,0),
                    new Vector3 (1,0,1)
        };

        triangles = new int[]
        {
            0, 1, 2,
            1, 3, 2
        };*/
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
        
    }
}
