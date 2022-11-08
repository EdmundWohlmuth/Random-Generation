using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    Mesh mesh;
    Vector3[] verticies;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenMesh()
    {
        // create verticies
        verticies = new Vector3[(width +1) * (depth + 1)];

        int i = 0;
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                verticies[i] = new Vector3(x, 0, z);
                i++;
            }

        }

        // create triangles
        triangles = new int[width * depth * 6];
        int tri = 0;
        int vert = 0;

        // BUG INVOLVING TWO TRIANGLE MESHES BEING CREATED ON THE UNDER SIDE OF THE TOP MESH
        // ITS VERTICIES ARE THE TWO LEFT MOST AND TWO RIGHT MOST VERTICIES
        for (int h = 0; h < depth; h++)
        {
            for (int v = 0; v < width; v++)
            {
                triangles[tri + 0] = h + vert + 0;
                triangles[tri + 1] = h + vert + width + 1;
                triangles[tri + 2] = h + vert + 1;
                
                triangles[tri + 3] = h + vert + 0;
                triangles[tri + 4] = h + vert + width;
                triangles[tri + 5] = h + vert + width + 1;

                tri += 6;
                vert++;
            }
        }


    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = verticies;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < verticies.Length; i++)
        {
            Gizmos.DrawSphere(verticies[i], 0.1f);
        }
        
    }
}
