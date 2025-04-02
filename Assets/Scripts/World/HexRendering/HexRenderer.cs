using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script for creating a hexagon mesh
/// generation of all verticies, faces
/// 
/// will likey swap to premade tiles later on
/// </summary>
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HexRenderer : MonoBehaviour
{
    private Mesh _mesh;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    private List<Face> _faces;

    private Material _material;

    public float innerSize = 1;
    public float outerSize = 1;
    public float height = 1;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();

        _mesh = new Mesh();
        _mesh.name = "Hex";

        _meshFilter.mesh = _mesh;
        _meshRenderer.material = _material;

        //shadows
        _meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        _meshRenderer.receiveShadows = false;


    }

    private void OnEnable()
    {
        DrawMesh();
    }

    public void DrawMesh()
    {
        DrawFaces();
        CombineFaces();
    }

    public void DrawFaces()
    {
        _faces = new List<Face>();

        //top faces
        for (int point = 0; point < 6; point++)
        {
            _faces.Add(CreateFace(innerSize, outerSize, height / 2f, height / 2f, point));
        }

        ////bottom faces
        //for (int point = 0; point < 6; point++)
        //{
        //    faces.Add(CreateFace(innerSize, outerSize, -height / 2f, -height / 2f, point, true));
        //}

        ////outer faces

        //for (int point = 0; point < 6; point++)
        //{
        //    faces.Add(CreateFace(outerSize, outerSize, height / 2f, -height / 2f, point, true));
        //}

        ////inner faces

        //for (int point = 0; point < 6; point++)
        //{
        //    faces.Add(CreateFace(innerSize, innerSize, height / 2f, -height / 2f, point, false));
        //}

    }

    private void CombineFaces()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < _faces.Count; i++)
        {
            vertices.AddRange(_faces[i].vertices);
            uvs.AddRange(_faces[i].uvs);

            int offset = (4 * i);
            foreach (int triangle in _faces[i].triangles)
            {
                triangles.Add(triangle + offset);
            }
        }

        _mesh.vertices = vertices.ToArray();
        _mesh.triangles = triangles.ToArray();
        _mesh.uv = uvs.ToArray();
        _mesh.RecalculateNormals();
    }

    private Face CreateFace(float innerRad, float outerRad, float heightA, float heightB, int point, bool reverse = false)
    {
        Vector3 pointA = GetPoint(innerRad, heightB, point);
        Vector3 pointB = GetPoint(innerRad, heightB, (point < 5) ? point + 1 : 0);
        Vector3 pointC = GetPoint(outerRad, heightA, (point < 5) ? point + 1 : 0);
        Vector3 pointD = GetPoint(outerRad, heightA, point);

        List<Vector3> vertices = new List<Vector3>() { pointA, pointB, pointC, pointD };
        List<int> triangles = new List<int>() { 0, 1, 2, 2, 3, 0 };
        List<Vector2> uvs = new List<Vector2>() { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };

        if (reverse)
        {
            vertices.Reverse();
        }

        return new Face(vertices, triangles, uvs);
    }

    protected Vector3 GetPoint(float size, float height, int index)
    {
        float angle_deg = 60 * index;
        float angle_rad = Mathf.PI / 180f * angle_deg;
        return new Vector3((size * Mathf.Cos(angle_rad)), height, size * Mathf.Sin(angle_rad));
    }

    public void SetMaterial(Material a_material)
    {
        _meshRenderer.material = a_material;
    }

    public void OnClickTile()
    {
        TileManager.instance.OnClickTile(this);
    }

    public void OnHoverTile()
    {
        TileManager.instance.OnHoverTile(this);
    }

}
