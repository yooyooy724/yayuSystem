using UnityEngine;
using System.Collections.Generic;

public class SphereGraphicTest_GL : MonoBehaviour
{
    public Material lineMaterial; // 描画に使用するマテリアル
    public float numberOfPointsPerFrame = 0.5f;
    private List<Vector3> points = new List<Vector3>();
    private float goldenRatio = (Mathf.Sqrt(5) + 1) / 2 - 1;
    private float goldenAngle => 2 * Mathf.PI * goldenRatio;
    private int totalPoints = 0;
    private float accumulatedPoints = 0f;

    void Update()
    {
        if (1f / Time.deltaTime >= 30f)
        {
            accumulatedPoints += numberOfPointsPerFrame;
            while (accumulatedPoints >= 1f)
            {
                AddPoint();
                accumulatedPoints -= 1f;
            }
        }
    }

    void AddPoint()
    {
        float longitude = goldenAngle * totalPoints;
        longitude /= 2 * Mathf.PI; longitude -= Mathf.Floor(longitude); longitude *= 2 * Mathf.PI;
        if (longitude > Mathf.PI) longitude -= 2 * Mathf.PI;

        float latitude = Mathf.Asin(-1 + 2 * totalPoints / (float)totalPoints);

        Vector3 position = SphereToCartesian(latitude, longitude);
        points.Add(position);

        totalPoints++;
    }

    Vector3 SphereToCartesian(float latitude, float longitude)
    {
        float R = 10; // 球の半径
        float x = R * Mathf.Cos(latitude) * Mathf.Cos(longitude);
        float y = R * Mathf.Cos(latitude) * Mathf.Sin(longitude);
        float z = R * Mathf.Sin(latitude);
        return new Vector3(x, y, z);
    }

    void OnRenderObject()
    {
        lineMaterial.SetPass(0);
        GL.Begin(GL.LINES);
        GL.Color(Color.white);

        foreach (Vector3 point in points)
        {
            GL.Vertex(point);
            GL.Vertex(point + Vector3.up * 0.1f); // 小さな線を描画して点を表現
        }

        GL.End();
    }

    void OnValidate()
    {
        // 素材が変更された場合、点の位置を更新
        points.Clear();
        totalPoints = 0;
        accumulatedPoints = 0f;
        UpdatePointsPositions();
    }

    void UpdatePointsPositions()
    {
        for (int i = 0; i < totalPoints; i++)
        {
            float longitude = goldenAngle * i;
            longitude /= 2 * Mathf.PI; longitude -= Mathf.Floor(longitude); longitude *= 2 * Mathf.PI;
            if (longitude > Mathf.PI) longitude -= 2 * Mathf.PI;

            float latitude = Mathf.Asin(-1 + 2 * i / (float)totalPoints);

            Vector3 position = SphereToCartesian(latitude, longitude);
            points[i] = position;
        }
    }
}
