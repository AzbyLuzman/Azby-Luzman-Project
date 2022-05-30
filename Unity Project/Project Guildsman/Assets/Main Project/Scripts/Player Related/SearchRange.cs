using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchRange : MonoBehaviour
{
    Mesh mesh;
    public bool meshActive;
    public LayerMask EnemyMask;
    [Range (10f, 20f)]
    public float sightRange = 15f;
    [Range(30f, 180f)]
    public float sightAngle = 60f;
    [Range(0.5f, 5f)]
    public float height = 2f;
    public bool enemyInSightRange;

    public CameraChange cameraChange;
    public List<string> enemyList = new List<string>();

    private void Start()
    {
        EnemyMask = LayerMask.GetMask("Enemy");
        cameraChange = GameObject.Find("Cameras").GetComponent<CameraChange>();
    }

    private void Update()
    {
        enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, EnemyMask);
    }

    public string LockOnEnemy()
    {
        int maxColliders = 10;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, sightRange, hitColliders, EnemyMask);
        for (int i = 0; i < numColliders; i++)
        {
            Vector3 direction = hitColliders[i].transform.position - transform.position;
            if (direction.y < 0 || direction.y > height)
            {
                continue;
            }

            direction.y = 0;
            float deltaAngle = Vector3.Angle(direction, transform.forward);
            if (deltaAngle > sightAngle)
            {
                continue;
            }

            if (!cameraChange.lockon)
            {
                return hitColliders[i].name;
            }
            else
            {
                enemyList.Add(hitColliders[i].name);
            }
        }

        return null;
    }

    public string MeasureSwitchDirection(bool direction, Transform currentEnemy)
    {
        string[] enemy = enemyList.ToArray();
        string switchTarget = null;
        float switchTargetAngle = 0f;

        for (int i = 0; i < enemy.Length; i++)
        {
            if (enemy[i] == currentEnemy.parent.name)
            {
                continue;
            }

            Vector3 targetDir = GameObject.Find(enemy[i]).transform.GetChild(0).position - transform.position;
            Vector3 forward = GameObject.Find(currentEnemy.parent.name).transform.position - transform.position;
            float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up);
            //Debug.Log(enemy[i] + ", Angle = " + angle);


            // Left Side
            if (!direction)
            {
                if (angle <= 0)
                {
                    continue;
                }
                else
                {
                    if (switchTarget == null)
                    {
                        switchTarget = enemy[i];
                        switchTargetAngle = angle;
                    }
                    else if (switchTargetAngle > angle)
                    {
                        switchTarget = enemy[i];
                        switchTargetAngle = angle;
                    }
                }
            }

            //Right Side
            else
            {
                if (angle >= 0)
                {
                    continue;
                }
                else
                {
                    if (switchTarget == null)
                    {
                        switchTarget = enemy[i];
                        switchTargetAngle = angle;
                    }
                    else if (switchTargetAngle < angle)
                    {
                        switchTarget = enemy[i];
                        switchTargetAngle = angle;
                    }
                }
            }
        }

        enemyList.Clear();

        if (switchTarget != null)
        {
            return switchTarget;
        }
        else
        {
            return null;
        }
    }

    Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();

        int numTriangles = 8;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -sightAngle, 0) * Vector3.forward * sightRange;
        Vector3 bottomRight = Quaternion.Euler(0, sightAngle, 0) * Vector3.forward * sightRange;

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

    private void OnValidate()
    {
        mesh = CreateMesh();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        if (mesh && meshActive)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }
    }
}
