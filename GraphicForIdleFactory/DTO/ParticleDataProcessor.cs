using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Unity.Burst;
using System;

namespace yayu.DOTS
{
    [BurstCompile]
    public struct AgeUpdateJob : IJobParallelFor
    {
        [ReadOnly] public float DeltaTime;
        [ReadOnly] public NativeArray<float> LifeTimes;
        public NativeArray<float> Ages;
        
        public void Execute(int index)
        {
            Ages[index] = Mathf.Min(Ages[index] + DeltaTime, LifeTimes[index]);
        }
    }

    [BurstCompile]
    public struct ColorUpdateJob : IJobParallelFor
    {
        public NativeArray<Color> TargetColors;
        [ReadOnly] public Color Color;

        public void Execute(int index)
        {
            TargetColors[index] = Color;
        }
    }

    [BurstCompile]
    public struct SpherePositionUpdateJob : IJobParallelFor
    {
        public NativeArray<Matrix4x4> Transforms;
        [ReadOnly] public float Radius;
        [ReadOnly] public int TotalPoints;

        public void Execute(int index)
        {
            float goldenAngle = Mathf.PI * (3 - Mathf.Sqrt(5)); // 黄金角
            float longitude = goldenAngle * index;
            longitude /= 2 * Mathf.PI; longitude -= Mathf.Floor(longitude); longitude *= 2 * Mathf.PI;
            if (longitude > Mathf.PI) longitude -= 2 * Mathf.PI;

            float latitude = Mathf.Asin(-1 + 2 * index / (float)TotalPoints);

            Vector3 position = SphereToCartesian(Radius, latitude, longitude);

            Matrix4x4 currentTransform = Transforms[index];
            Transforms[index] = Matrix4x4.TRS(position, currentTransform.rotation, currentTransform.lossyScale);
        }

        private static Vector3 SphereToCartesian(float Radius, float latitude, float longitude)
        {
            float R = Radius;
            float x = R * Mathf.Cos(latitude) * Mathf.Cos(longitude);
            float y = R * Mathf.Cos(latitude) * Mathf.Sin(longitude);
            float z = R * Mathf.Sin(latitude);
            return new Vector3(x, y, z);
        }
    }


    public class ParticleDataProcessor
    {
        private readonly ParticlesDataList particlesData;

        public ParticleDataProcessor(ParticlesDataList particlesData)
        {
            this.particlesData = particlesData;
        }

        public void UpdateAges(float deltaTime)
        {
            if(particlesData.GetAgeAsArray().Length <= 0) return;
            var job = new AgeUpdateJob
            {
                DeltaTime = deltaTime,
                LifeTimes = particlesData.GetLifeTimeAsArray(),
                Ages = particlesData.GetAgeAsArray(),
            };

            var jobHandle = job.Schedule(particlesData.GetAgeAsArray().Length, 64);
            jobHandle.Complete();
        }

        public void UpdateColors(Color color)
        {
            if (particlesData.GetAgeAsArray().Length <= 0) return;
            var job = new ColorUpdateJob
            {
                TargetColors = particlesData.GetColorAsArray(),
                Color = color,
            };

            var jobHandle = job.Schedule(particlesData.GetColorAsArray().Length, 64);
            jobHandle.Complete();
        }

        public void UpdateTransform(float radius)
        {
            if (particlesData.GetAgeAsArray().Length <= 0) return;
            var job = new SpherePositionUpdateJob
            {
                Radius = radius,
                Transforms = particlesData.GetTransformAsArray(),
                TotalPoints = particlesData.GetTransformAsArray().Length
            };

            var jobHandle = job.Schedule(particlesData.GetTransformAsArray().Length, 64);
            jobHandle.Complete();

        }

        public void UpdateFirstParticleRate(float rate)
        {
            Color col = new Color(1, 1, 0, rate);
            particlesData.TrySetColor(particlesData.GetColorAsArray().Length - 1, col);
        }
    }

}
