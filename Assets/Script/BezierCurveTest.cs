using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BezierCurveTest : MonoBehaviour
{
    // 点的半径
    public float radius = 1;
    // 曲线取点的密度
    public int densityCurve = 1000;
    public List<GameObject> ControlPointList = new List<GameObject>();
    public List<Vector3> CurvePointList = new List<Vector3>();


    private void OnDrawGizmos()
    {
        // 绘制前重新添加控制点
        ControlPointList.Clear();
        foreach (Transform item in transform)
        {
            ControlPointList.Add(item.gameObject);
        }

        // Select 取每个点的position作为新的元素
        List<Vector3> controlPointPos = ControlPointList.Select(point => point.transform.position).ToList();
        // 经过三阶运算返回的需要绘制的点
        var points = GetDrawingPoints(controlPointPos, densityCurve);

        Vector3 startPos = points[0];
        CurvePointList.Clear();
        CurvePointList.Add(startPos);
        for (int i = 1; i < points.Count; i++)
        {
            if (Vector3.Distance(startPos, points[i]) >= radius)
            {
                startPos = points[i];
                CurvePointList.Add(startPos);
            }
        }

        //绘制曲线
        Gizmos.color = Color.blue;
        foreach (var item in CurvePointList)
        {
            Gizmos.DrawSphere(item, radius * 0.5f);
        }

        //绘制曲线控制点连线
        Gizmos.color = Color.red;
        for (int i = 0; i < controlPointPos.Count - 1; i++)
        {
            Gizmos.DrawLine(controlPointPos[i], controlPointPos[i + 1]);
        }

    }


    public List<Vector3> GetDrawingPoints(List<Vector3> controlPoints, int segmentsPerCurve)
    {
        List<Vector3> points = new List<Vector3>();
        // 下一段的起始点和上段终点是一个，所以是 i+=3
        for (int i = 0; i < controlPoints.Count - 3; i += 3)
        {

            var p0 = controlPoints[i];
            var p1 = controlPoints[i + 1];
            var p2 = controlPoints[i + 2];
            var p3 = controlPoints[i + 3];

            for (int j = 0; j <= segmentsPerCurve; j++)
            {
                var t = j / (float)segmentsPerCurve;
                points.Add(CalculateBezierPoint(t, p0, p1, p2, p3));
            }
        }
        return points;
    }

    // 三阶公式
    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3 result;

        Vector3 p0p1 = (1 - t) * p0 + t * p1;
        Vector3 p1p2 = (1 - t) * p1 + t * p2;
        Vector3 p2p3 = (1 - t) * p2 + t * p3;

        Vector3 p0p1p2 = (1 - t) * p0p1 + t * p1p2;
        Vector3 p1p2p3 = (1 - t) * p1p2 + t * p2p3;

        result = (1 - t) * p0p1p2 + t * p1p2p3;
        return result;
    }

}