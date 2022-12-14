using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// WISH LIST:

// - Dynamic texture / material based on height
// - Water & water level
// - Looping through multiple meshes to create "chunks"
// - sync the perlin noise between "chunks"
// - Use subscenes for preformance

public class MapGen : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    [Header("World Size")]
    public int width;
    public int depth;

    [Header("Perlin Noise")]
    [Range(0f, 20f)]
    public float amplitude;
    [Range(0f, 0.5f)]
    public float frequency;

    [Header("Seed variables")]
    [Range(-25f, 25f)]
    public float offsetX;
    public float offsetZ;
    [Range(1, 5)]
    public int smoothing;

    [Header("Water variables")]
    [Range(0f, 20f)]
    public float waterLevel;
    public GameObject water;
    public Material waterMat;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        mesh = GetComponent<MeshFilter>().mesh;

        water.transform.localScale = new Vector3(5, 1, 5);
        GenMesh();    
    }

    private void Update()
    {
        WaterMesh();
        UpdateMesh();
        if (Input.GetKey(KeyCode.Space))
        {
            NewMesh();
        }      
    }

    float PerlinNoise(float x, float y) // -1.0...1.0
    {
        return -Mathf.Abs(Mathf.PerlinNoise(x, y * 2f) - 1) * amplitude; // doubles amplitude
    }

    float Spectral(float x, float z, float frequency, float amplitude)
    {
        float result = 0;
        result += Mathf.PerlinNoise(x * frequency, z * frequency) * amplitude;// w/ variation
        result += Mathf.PerlinNoise(x * frequency * 2, z * frequency * 2) * amplitude / 2;
        result += Mathf.PerlinNoise(x * frequency * 3, z * frequency * 3) * amplitude / 3;
        //result += Mathf.PerlinNoise(x * frequency * 8, z * frequency * 8) * amplitude / 8;
        //result += Mathf.PerlinNoise(x * frequency * 2, z * frequency * 2) * amplitude / 16;
        return result;
        
        // take abs then invert -- abs aplies turbulence (-abs PerlinNoise)
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
                float y = Spectral(x + offsetX, z + offsetZ, frequency, amplitude); // PerlinNoise map - could modify x and y
                vertices[i] = new Vector3(x, y, z);                                    // for diffrent maps
               // Debug.Log(i);
                i++;
            }

        }

        // --------------------------- create triangles ------------------------------- \\

        // create triangles
        triangles = new int[width * depth * 6];
        int activeVertex = 0;
        int lineNum = (width * 6) - 6;
        for (int x = 0; x < width * depth - 1; x++)
        {
            
            if (activeVertex == lineNum)
            {
                lineNum += width * 6; // hardcoded for 11 * 11 - Theoretically it can be width * 6 plus or minus something
                activeVertex += 6;
            }
            else if (activeVertex == (width * depth * 6) - width * 6)
            {
                break;
            }
            else
            {
                // triangle 1

                triangles[activeVertex] = x; // a
                activeVertex++;

                triangles[activeVertex] = x + width; // because this vertex is on another row we add the width to get it // b
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
            }
        }

        /*        for (int z = 0; z < depth; z++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int a = x + ((width + 1) * z);
                        int b = a + 1;
                        int c = a + width + 1;
                        int d = b + width + 1;

                        // triangle 1
                        triangles[activeVertex] = a;
                        triangles[activeVertex] = b;
                        triangles[activeVertex] = c;

                        // triangle 2
                        triangles[activeVertex] = b;
                        triangles[activeVertex] = d;
                        triangles[activeVertex] = c;
                    }
                }*/

        // a = x + ((width + 1) * z)
        // b = a + 1
        // c = a + width + 1
        // d = b = width + 1

        // vertex loop breaks at the very end causing the next vertex to be all the way at "0"
        // need to set up a way to prevent the far edges from creating a triangle



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

    void NewMesh()
    {
        mesh.Clear();
        GenMesh();
    }

    void WaterMesh()
    {
        water.transform.position = new Vector3(width / 2, waterLevel, depth / 2);
    }

/*
    private void OnDrawGizmos()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
        
    }*/
}
