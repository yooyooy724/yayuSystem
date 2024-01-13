using UnityEngine;
using System.Collections.Generic;

public class SphereGraphicTest : MonoBehaviour
{
    public GameObject pointPrefab; // 点を表すためのプレハブ
    public float numberOfPointsPerFrame = 0.5f; // 1フレームあたりに追加する点の数（少数可）
    private List<GameObject> points = new List<GameObject>();
    private float goldenRatio = (Mathf.Sqrt(5) + 1) / 2 - 1;
    private float goldenAngle => 2 * Mathf.PI * goldenRatio;
    private int totalPoints = 0; // 生成された総点数
    private float accumulatedPoints = 0f; // 累積点数

    void Update()
    {
        if (1f / Time.deltaTime >= 30f) // FPSが30以上の場合のみ処理
        {
            accumulatedPoints += numberOfPointsPerFrame;
            while (accumulatedPoints >= 1f)
            {
                AddPoint();
                accumulatedPoints -= 1f;
            }
            UpdatePointsPositions(); // 点の位置を更新
        }
    }

    void AddPoint()
    {
        GameObject point = Instantiate(pointPrefab);
        points.Add(point);
        totalPoints++; // 総点数を更新
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
            points[i].transform.position = position;
        }
    }

    Vector3 SphereToCartesian(float latitude, float longitude)
    {
        float R = 10; // 球の半径
        float x = R * Mathf.Cos(latitude) * Mathf.Cos(longitude);
        float y = R * Mathf.Cos(latitude) * Mathf.Sin(longitude);
        float z = R * Mathf.Sin(latitude);
        return new Vector3(x, y, z);
    }
}
