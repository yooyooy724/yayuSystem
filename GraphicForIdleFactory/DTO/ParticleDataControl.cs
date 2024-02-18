using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace My.DOTS
{
    public interface IChangeablePositionGetter
    {
        int GetPositionId();
        Matrix4x4 GetTransform(int positionId);
    }

    public interface IParticleDataAccessor
    {
        NativeArray<Matrix4x4> GetTransformAsArray();
        NativeArray<Color> GetColorAsArray();
        int Count();
        public int MaxCount { get; }
    }

    public class ParticlesDataListControl : IDisposable
    {
        private ParticlesDataList particlesDataList;
        public ParticlesDataList ParticlesDataList => particlesDataList;

        readonly IParticleData spawnParticleData;

        public int maxParticleCount { get; }

        public ParticlesDataListControl(
            IParticleData spawnParticleData,
            int maxParticleCount,
            Matrix4x4 parentTransform)
        {
            this.spawnParticleData = spawnParticleData;
            this.maxParticleCount = maxParticleCount;
            particlesDataList = new ParticlesDataList(parentTransform, maxParticleCount);
        }

        public bool IsParticleActive(int index)
        {
            return particlesDataList.IsParticleAtIndexActive(index);
        }

        /// <summary>
        /// return index. when fail to add, return -1
        /// </summary>
        public bool Add()
        {
            // Check if the buffer is full
            if (particlesDataList.IsMaxSizeReached())
            {
                return false;  // Buffer is full
            }
            particlesDataList.AddParticle(spawnParticleData);
            return true;
        }

        public bool TryGetNewestParticle(out ParticleDataStruct particleData)
        {
            return particlesDataList.TryGetParticleData(particlesDataList.Count() - 1, out particleData);
        }

        public bool TryRemoveOldest(out ParticleDataStruct removedParticle)
        {
            if (particlesDataList.TryGetOldestParticleData(out removedParticle))
            {
                particlesDataList.RemoveFirstParticle();
                return true;
            }

            removedParticle = default;  // Set the out parameter to its default value
            return false;  // Indicate that no particle was removed
        }

        public bool TryRemove(int index, out ParticleDataStruct removedParticle)
        {
            if (particlesDataList.TryGetParticleData(index, out removedParticle))
            {
                return particlesDataList.RemoveParticle(index);
            }

            removedParticle = default;  // Set the out parameter to its default value
            return false;  // Indicate that no particle was removed
        }

        public int ParticleCount => particlesDataList.Count();

        public bool IsOldestParticleExpired()
        {
            return particlesDataList.IsOldestParticleExpired();
        }

        private List<ParticleDataStruct> removeExpiredParticlesList = new List<ParticleDataStruct>();

        public IEnumerable<ParticleDataStruct> RemoveExpiredParticles()
        {
            removeExpiredParticlesList.Clear();  // Clear the list for reuse

            while (IsOldestParticleExpired())
            {
                if (TryRemoveOldest(out ParticleDataStruct removedParticle))
                {
                    removeExpiredParticlesList.Add(removedParticle);
                }
                else
                {
                    break;  // If we fail to remove the oldest particle, exit the loop
                }
            }

            return removeExpiredParticlesList.Count == 0 ? null : removeExpiredParticlesList;
        }

        private List<ParticleDataStruct> removedParticlesList_WithinCircle = new List<ParticleDataStruct>();

        public IEnumerable<ParticleDataStruct> RemoveParticlesWithinCircle(ICircleDataAccessor circleData)
        {

            if (!circleData.IsActive())
                return null;

            removedParticlesList_WithinCircle.Clear();  // Clear the list for reuse
            
            Vector2 circlePosition = circleData.Position;
            float circleRadius = circleData.Radius;

            for (int i = particlesDataList.Transforms.Length - 1; i >= 0; i--)
            {
                Vector2 particlePosition = new Vector2(particlesDataList.Transforms[i].m03, particlesDataList.Transforms[i].m13);  // Assuming m03 and m13 are x and y components respectively.

                if (Vector2.Distance(particlePosition, circlePosition) <= circleRadius)
                {
                    if (TryRemove(i, out ParticleDataStruct removedParticle))
                    {
                        removedParticlesList_WithinCircle.Add(removedParticle);
                    }
                    else
                    {
                        YDebugger.LogWarning("‚»‚ñ‚È‚±‚Æ‚ ‚éH");
                    }
                }
            }

            return removedParticlesList_WithinCircle.Count == 0 ? null : removedParticlesList_WithinCircle;
        }

        private List<ParticleDataStruct> removedAllParticlesList = new List<ParticleDataStruct>();

        public IEnumerable<ParticleDataStruct> RemoveAllParticles()
        {
            removedAllParticlesList.Clear();

            for (int index = 0; index < maxParticleCount; index++)
            {
                if (IsParticleActive(index))
                {
                    if (particlesDataList.TryGetParticleData(index, out ParticleDataStruct removedParticle)) 
                    {
                        particlesDataList.RemoveParticle(index);
                        removedAllParticlesList.Add(removedParticle);
                    }
                    else
                    {
                        YDebugger.LogWarning("IsParticleActive(index) ‚ªTrue‚Å@TryGet‚ªFalse‚È‚Ì‚Í‚ß‚¿‚á‚­‚¿‚á‰ö‚µ‚¢ !?!?!?!?!?!?!?");
                    }
                }
            }

            return removedAllParticlesList;
        }

        public void Clear()
        {
            particlesDataList.Clear();
        }

        public void Dispose()
        {
            particlesDataList.Dispose();
        }

    }
}