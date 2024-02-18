using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace My.DOTS
{
    public interface IParticleData
    {
        int Index { get; }
        Matrix4x4 Transform { get; }
        float LifeTime { get; }
        Color Color { get; }
        float Age { get; }
    }

    public struct ParticleDataStruct: IParticleData
    {
        public int Index { get; set; }
        public Matrix4x4 Transform { get; set; }
        public float LifeTime { get; set; }
        public Color Color { get; set; }
        public float Age { get; set; }
    }

    public class ParticlesDataList: IParticleDataAccessor, IDisposable
    {
        Matrix4x4 parentTransform;
        public ParticlesDataList(Matrix4x4 parentTransform, int maxSize)
        {
            this.parentTransform = parentTransform;
            this.maxSize = maxSize;

            Transforms = new NativeList<Matrix4x4>(maxSize, Allocator.Persistent);
            Ages = new NativeList<float>(maxSize, Allocator.Persistent);
            LifeTimes = new NativeList<float>(maxSize, Allocator.Persistent);
            Colors = new NativeList<Color>(maxSize, Allocator.Persistent);
        }

        public NativeList<Matrix4x4> Transforms { get; private set; }
        NativeList<float> Ages;
        NativeList<float> LifeTimes;
        NativeList<Color> Colors;

        private int maxSize; // 最大要素数
        public int MaxCount => maxSize;

        public NativeArray<Matrix4x4> GetTransformAsArray() => Transforms.AsArray();
        public NativeArray<Color> GetColorAsArray() => Colors.AsArray();
        public NativeArray<float> GetAgeAsArray() => Ages.AsArray();
        public NativeArray<float> GetLifeTimeAsArray() => LifeTimes.AsArray();

        public int Count() => Transforms.Length;
        
        public bool IsOldestParticleExpired() => Transforms.Length > 0 && Ages[0] >= LifeTimes[0];

        public bool TryGetTransform(int index, out Matrix4x4 transform)
        {
            if (index < 0 || index >= Transforms.Length)
            {
                transform = default;
                return false; // Index out of range
            }
            transform = Transforms[index];
            return true;
        }
        public bool TryGetColor(int index, out Color color)
        {
            if (index < 0 || index >= Colors.Length)
            {
                color = default;
                return false; // Index out of range
            }
            color = Colors[index];
            return true;
        }

        public bool TrySetColor(int index, Color color)
        {
            if (index < 0 || index >= Colors.Length || Colors.Length <= 0)
            {
                return false; // Index out of range
            }
            Colors[index] = color;
            return true;
        }

        public bool TrySetAlpha(int index, float alpha)
        {
            if (index < 0 || index >= Colors.Length || Colors.Length <= 0)
            {
                return false; // Index out of range
            }
            Color col = Colors[index];
            col.a = alpha;
            Colors[index] = col;
            return true;
        }

        public bool TryGetParticleData(int index, out ParticleDataStruct particleData)
        {
            if (index < 0 || index >= Transforms.Length)
            {
                particleData = default;
                return false; // Index out of range
            }

            particleData = new ParticleDataStruct
            {
                Index = index,
                Transform = Transforms[index],
                LifeTime = LifeTimes[index],
                Color = Colors[index],
                Age = Ages[index],
            };
            return true;
        }
        public bool TryGetOldestParticleData(out ParticleDataStruct particleData)
        {
            if (Transforms.Length == 0)
            {
                particleData = default;
                return false; // No elements to fetch
            }

            particleData = new ParticleDataStruct
            {
                Index = 0, // Oldest particle will always be at index 0
                Transform = Transforms[0],
                LifeTime = LifeTimes[0],
                Color = Colors[0],
                Age = Ages[0]
            };

            return true;
        }

        public bool IsParticleAtIndexActive(int index)
        {
            return (index >= 0 && index < Transforms.Length);
        }

        public void Dispose()
        {
            // Dispose of NativeLists
            Transforms.Dispose();
            Ages.Dispose();
            LifeTimes.Dispose();
            Colors.Dispose();
        }

        public bool IsMaxSizeReached()
        {
            return Transforms.Length >= maxSize;
        }

        public bool AddParticle(IParticleData particleData)
        {
            if (Transforms.Length >= maxSize)
            {
                return false;
            }

            Transforms.Add(parentTransform * particleData.Transform);
            Ages.Add(particleData.Age);
            LifeTimes.Add(particleData.LifeTime);
            Colors.Add(particleData.Color);
            return true;
        }

        public bool RemoveParticle(int index)
        {
            if (index < 0 || index >= Transforms.Length)
            {
                return false; // Index out of range
            }

            Transforms.RemoveAt(index);
            Ages.RemoveAt(index);
            LifeTimes.RemoveAt(index);
            Colors.RemoveAt(index);

            return true;
        }

        public bool RemoveFirstParticle()
        {
            if (Transforms.Length == 0)
            {
                return false; // No elements to remove
            }

            Transforms.RemoveAt(0);
            Ages.RemoveAt(0);
            LifeTimes.RemoveAt(0);
            Colors.RemoveAt(0);

            return true;
        }

        public void Clear()
        {
            Transforms.Clear();
            Ages.Clear();
            LifeTimes.Clear();
            Colors.Clear();
        }
    }
}
