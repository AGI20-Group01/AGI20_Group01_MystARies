using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private float size = 1f;

    public float Size { get { return size; } }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;

        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float x = 0; x < 10; x += size)
        {
            for (float z = 0; z < 10; z += size)
            {
                for (float y = 0; y < 10; y += size)
                {
                    var point = GetNearestPointOnGrid(new Vector3(x,y,z));
                    Gizmos.DrawSphere(point, 0.1f);
                }
            }
        }
    }
}
