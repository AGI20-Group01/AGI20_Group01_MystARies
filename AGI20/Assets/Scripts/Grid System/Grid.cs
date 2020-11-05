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
        //position = transform.rotation * position;
        position -= transform.position;
        position = Quaternion.Inverse(transform.rotation) * position;
        //position += new Vector3(5,5,5);




        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);
        

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result = transform.rotation * result;
        result += transform.position;

        return result;
    }

    public Vector3 GetGridCellIndex(Vector3 position) {
        position -= transform.position;
        position = Quaternion.Inverse(transform.rotation) * position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        
        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float x = -5; x < 6; x += size)
        {
            for (float z = -5; z < 6; z += size)
            {
                for (float y = -5; y < 6; y += size)
                {
                    var point = GetNearestPointOnGrid(new Vector3(x,y,z));
                    Gizmos.DrawSphere(point, 0.1f);
                }
            }
        }
    }
}

