using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TrackGraphic : MonoBehaviour
{
    public float RaycastRadius;
    public float Width;
    public float Interval;
    public int RaycastSimplify;
    public LayerMask Mask;
    public float Gravity;
    [Range(100,500)]
    public int PointsCount = 100;
    public Vector3 velocity;
    private Vector3 pos;

    public MeshFilter TrackRender;
    public MeshFilter EndRender;


   

    
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        CreateTrack();
    }

    // Update is called once per frame
    void Update()
    {
        //CreateTrack();
        //if (pos.y < -1)
        //{
        //    velocity = new Vector3(5, 5, 5);
        //    pos = new Vector3(0, 0, 0);
        //}
    }

    void CreateTrack()
    {
        
        List<Vector3> Points = new List<Vector3>();

        Vector3 endPos = Vector3.zero;

        RaycastHit hitInfo;
        for (int i = 0; i < PointsCount; i++)
        {
            Points.Add(pos);
            if (i != 0 && i % RaycastSimplify == 0)
            {
                Vector3 dirVec = pos - Points[i - RaycastSimplify];
                if (Physics.SphereCast(Points[i - RaycastSimplify], RaycastRadius, dirVec.normalized, out hitInfo, dirVec.magnitude, Mask.value))
                {
                    endPos = hitInfo.point;
                    break;
                }
            }
            velocity += Vector3.down * Gravity * Interval;
            pos += velocity * Interval;
        }

        meshdata data = new meshdata(Points, Width);
        TrackRender.mesh = data.CreateMesh();
    }

    private void OnDrawGizmos()
    {
        
    }
}
