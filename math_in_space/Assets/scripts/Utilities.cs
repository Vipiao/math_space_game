using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static void getBoundaries(out Vector3 lowerLeft, out Vector3 upperRight)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.main.orthographicSize;
        lowerLeft = Camera.main.transform.position -
            new Vector3(cameraHeight * screenAspect, 0, cameraHeight);
        upperRight = Camera.main.transform.position +
            new Vector3(cameraHeight * screenAspect, 0, cameraHeight);
        //Debug.Log("(float)Screen.width" + (float)Screen.width);
        //Debug.Log("(float)Screen.height" + (float)Screen.height);
        //Debug.Log("lowerLeft" + lowerLeft);
        //Debug.Log("upperRight" + upperRight);
    }
    public static Vector3 limitWithinBounds(Vector3 pos)
    {
        Vector3 lowerLeft;
        Vector3 upperRight;
        Utilities.getBoundaries(out lowerLeft, out upperRight);
        float x, y, z;
        x = pos.x;
        y = pos.y;
        z = pos.z;
        if (pos.x > upperRight.x)
        {
            x = pos.x - upperRight.x + lowerLeft.x;

        }
        else if (pos.x < lowerLeft.x)
        {
            x = pos.x + upperRight.x - lowerLeft.x;
        }
        if (pos.z > upperRight.z)
        {
            z = pos.z - upperRight.z + lowerLeft.z;
        }
        else if (pos.z < lowerLeft.z)
        {
            z = pos.z + upperRight.z - lowerLeft.z;
        }
        return new Vector3(x, y, z);
    }
    public static Quaternion turnTowards(Vector3 target, float factor)
    {
        Quaternion rotation = Quaternion.FromToRotation(
            target,
            Vector3.Lerp(target, Vector3.up, factor)
        );
        return rotation;
    }
}
