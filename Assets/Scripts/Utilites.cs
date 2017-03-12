using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilites
{
    public static float GetDistance(Vector3 pos1, Vector3 pos2)
    {
        return Vector3.Distance(pos1, pos2);
    }
    public static Transform GetClosestObjectTo(List<Transform> listofObj, Transform toObj)
    {
        var bestdist = Mathf.Infinity;
        Transform tran = listofObj[0];
        for (int i = 0; i < listofObj.Count; i++)
        {
            if (toObj != null && listofObj[i] != null)
            {
                var distance = GetDistance(toObj.position, listofObj[i].position);
                if (bestdist > distance)
                {
                    bestdist = distance;
                    tran = listofObj[i];

                }
            }
           

        }
        Debug.Log("Closest object is " + tran.name);
        return tran;
    }

}
