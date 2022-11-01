using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BezierCurveTest : MonoBehaviour
{
    // ��İ뾶
    public float radius = 1;
    // ����ȡ����ܶ�
    public int densityCurve = 1000;
    public List<GameObject> ControlPointList = new List<GameObject>();
    public List<Vector3> CurvePointList = new List<Vector3>();


    private void OnDrawGizmos()
    {
        // ����ǰ������ӿ��Ƶ�
        ControlPointList.Clear();
        foreach (Transform item in transform)
        {
            ControlPointList.Add(item.gameObject);
        }

        // Select ȡÿ�����position��Ϊ�µ�Ԫ��
        List<Vector3> controlPointPos = ControlPointList.Select(point => point.transform.position).ToList();
        // �����������㷵�ص���Ҫ���Ƶĵ�
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

        //��������
        Gizmos.color = Color.blue;
        foreach (var item in CurvePointList)
        {
            Gizmos.DrawSphere(item, radius * 0.5f);
        }

        //�������߿��Ƶ�����
        Gizmos.color = Color.red;
        for (int i = 0; i < controlPointPos.Count - 1; i++)
        {
            Gizmos.DrawLine(controlPointPos[i], controlPointPos[i + 1]);
        }

    }


    public List<Vector3> GetDrawingPoints(List<Vector3> controlPoints, int segmentsPerCurve)
    {
        List<Vector3> points = new List<Vector3>();
        // ��һ�ε���ʼ����϶��յ���һ���������� i+=3
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

    // ���׹�ʽ
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