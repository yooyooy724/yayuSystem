using UnityEngine;
using System.Collections.Generic;

public class SphereGraphicTest : MonoBehaviour
{
    public GameObject pointPrefab; // �_��\�����߂̃v���n�u
    public float numberOfPointsPerFrame = 0.5f; // 1�t���[��������ɒǉ�����_�̐��i�����j
    private List<GameObject> points = new List<GameObject>();
    private float goldenRatio = (Mathf.Sqrt(5) + 1) / 2 - 1;
    private float goldenAngle => 2 * Mathf.PI * goldenRatio;
    private int totalPoints = 0; // �������ꂽ���_��
    private float accumulatedPoints = 0f; // �ݐϓ_��

    void Update()
    {
        if (1f / Time.deltaTime >= 30f) // FPS��30�ȏ�̏ꍇ�̂ݏ���
        {
            accumulatedPoints += numberOfPointsPerFrame;
            while (accumulatedPoints >= 1f)
            {
                AddPoint();
                accumulatedPoints -= 1f;
            }
            UpdatePointsPositions(); // �_�̈ʒu���X�V
        }
    }

    void AddPoint()
    {
        GameObject point = Instantiate(pointPrefab);
        points.Add(point);
        totalPoints++; // ���_�����X�V
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
        float R = 10; // ���̔��a
        float x = R * Mathf.Cos(latitude) * Mathf.Cos(longitude);
        float y = R * Mathf.Cos(latitude) * Mathf.Sin(longitude);
        float z = R * Mathf.Sin(latitude);
        return new Vector3(x, y, z);
    }
}
