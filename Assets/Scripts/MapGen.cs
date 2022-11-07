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

    }

    void UpdateMesh()
    {

    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < verticies.Length; i++)
        {
            Gizmos.DrawSphere(verticies[i], 0.1f);
        }
        
    }
}
