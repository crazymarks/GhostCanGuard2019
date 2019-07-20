using System.Collections.Generic;
using UnityEngine;

public class meshdata
{
    public Vector3 Up = Vector3.up;
    public Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;

    private int vertexIndex;
    private int triangleIndex;

    private float Width;
    private List<Vector3> Line;

    Color[] colors;

    public meshdata(List<Vector3> line,float width)
    {
        Line = line;
        Width = width;

        vertices = new Vector3[Line.Count * 2];
        uvs = new Vector2[Line.Count * 2];
        triangles = new int[(Line.Count - 1) * 6];
        vertexIndex = triangleIndex = 0;
        colors = new Color[Line.Count * 2];

        int length = Line.Count;
        for(int i = 0; i < length; i++)
        {
            vertices[vertexIndex] = Line[i] + Up * Width;           //線の一側から点を作る
            vertices[vertexIndex + length] = Line[i] - Up * Width;      //もう一つ側から点を作る

            uvs[vertexIndex] = new Vector2(i / (float)length, 1);
            uvs[vertexIndex + length] = new Vector2(i / (float)length ,1);

            colors[vertexIndex] = Color.cyan;
            colors[vertexIndex + length] = Color.cyan;

            if (i < length - 1)
            {
                AddTriangle(vertexIndex, vertexIndex + length + 1, vertexIndex + length);
                AddTriangle(vertexIndex + length + 1, vertexIndex, vertexIndex + 1);
            }
            
            vertexIndex++;
        }
    }


    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.name = "trails";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.colors = colors;
        return mesh;
    }

    void AddTriangle(int a,int b,int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;

        triangleIndex += 3;
    }
}
