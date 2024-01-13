using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yayu.DTO
{
    public class ParticleManager : MonoBehaviour
    {
        [SerializeField] int _particleCount = 100;
        [SerializeField] int _maxParticleCount = 10000;
        [SerializeField] float _radius = 10;
        [SerializeField] ParticleRenderer particleRenderer;
        [SerializeField] Transform parent;
        int ParticleCount => Mathf.Clamp(_particleCount, 0, _maxParticleCount);
        int MaxParticleCount => _maxParticleCount;
        float Radius => _radius;

        ParticlesDataList particlesDataList;
        ParticleDataProcessor particleDataProcessor;
        IDisposable disposable => particlesDataList;
        

        void Start()
        {
            particlesDataList = new ParticlesDataList(parent.localToWorldMatrix, MaxParticleCount);
            particleDataProcessor = new ParticleDataProcessor(particlesDataList);
            particleRenderer.Init(particlesDataList, "_Sphere");
        }

        // Update is called once per frame
        void Update()
        {
            bool changeParticleCount = false;

            while (particlesDataList.Count() < ParticleCount)
            {
                particlesDataList.AddParticle(NewParticleData());
                changeParticleCount = true;
            }
            while (particlesDataList.Count() > ParticleCount)
            {
                particlesDataList.RemoveFirstParticle();
                changeParticleCount = true;
            }

            if (changeParticleCount)
            {
                particleDataProcessor.UpdateColors(Color.white);
                particleDataProcessor.UpdateTransform(Radius);
            }

            particleDataProcessor.UpdateAges(Time.deltaTime);
        }

        IParticleData NewParticleData()
        {
            return new ParticleDataStruct()
            {
                Index = particlesDataList.Count(),
                Transform = Matrix4x4.identity,
                LifeTime = 100,
                Color = Color.white,
                Age = 0,
            };
        }

        void OnDestroy()
        {
            disposable.Dispose();
        }
    }

}