using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [Header("Enemy Behaviour")]
    public EnemyBehaviour enemyBehaviour;
    [Header("Field of View")]
    public bool meshActive;
    Mesh mesh;
    public float angle = 30f;
    public float height = 2f;
    public bool inView, detected;

    private void Start()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
    }

    private void Update()
    {
        SetTarget();
    }

    #region Target
    void SetTarget()
    {
        if (inView)
        {
            detected = DetectPlayer();
        }
    }

    bool DetectPlayer()
    {
        int maxColliders = 5;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, enemyBehaviour.sightRange, hitColliders, enemyBehaviour.partyMask + enemyBehaviour.playerMask);
        for (int i = 0; i < numColliders; i++)
        {
            Vector3 direction = hitColliders[i].transform.position - transform.position;
            direction.y = 0;
            float deltaAngle = Vector3.Angle(direction, transform.forward);
            if (deltaAngle > angle)
            {
                continue;
            }

            if (enemyBehaviour.target == null)
            {
                enemyBehaviour.target = hitColliders[i].gameObject;
                Debug.Log(enemyBehaviour.target.name);
            }


            // ADJUST TARGET FOR MULTIPLE TARGET

        }

        if (enemyBehaviour.target != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion


    #region Field of View

    Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();

        int numTriangles = 8;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * enemyBehaviour.sightRange;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * enemyBehaviour.sightRange;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;

        int vert = 0;

        //左側
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        //右側
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        //前側
        vertices[vert++] = bottomLeft;
        vertices[vert++] = bottomRight;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = topLeft;
        vertices[vert++] = bottomLeft;

        //上側
        vertices[vert++] = topCenter;
        vertices[vert++] = topLeft;
        vertices[vert++] = topRight;

        //下側
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomLeft;

        for (int i = 0; i < numVertices; ++i)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    private void OnDrawGizmos()
    {
        if (mesh && meshActive)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }
    }

    #endregion

    private void OnValidate()
    {
        mesh = CreateMesh();
    }
}
