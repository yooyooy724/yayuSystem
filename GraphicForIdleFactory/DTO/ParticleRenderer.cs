using UnityEngine;

namespace yayu.DOTS
{
    public class ParticleRenderer : MonoBehaviour
    {
        [SerializeField] GameObject instancePrefab;   // インスタンス化するオブジェクトのプレハブ

        IParticleDataAccessor particleData;  // Assume you have a method to populate this
        string id;
        private const int maxInstancesPerDraw = 400;

        private Mesh mesh;
        private Material material;
        private MaterialPropertyBlock matProps;
        private GraphicsBuffer colorBuffer;

        public void Init(IParticleDataAccessor particleData, string id)
        {
            this.particleData = particleData;
            this.id = id;
            mesh = instancePrefab.GetComponent<MeshFilter>().sharedMesh;
            material = instancePrefab.GetComponent<Renderer>().sharedMaterial;
            if (matProps == null) matProps = new MaterialPropertyBlock();

            if (colorBuffer == null || colorBuffer.count != particleData.MaxCount)
            {
                colorBuffer = new GraphicsBuffer
                    (GraphicsBuffer.Target.Structured,
                    particleData.MaxCount, sizeof(float) * 4);
            }
        }


        public void Deactivate()
        {
            this.particleData = null;
        }

        void Update()
        {
            if (particleData == null || particleData.Count() <= 0) return;
            RenderParticles();
        }

        void RenderParticles()
        {
            int offsetID = 0;
            int activeParticleCount = particleData.Count();
            var matrices = particleData.GetTransformAsArray();
            colorBuffer.SetData(particleData.GetColorAsArray());
            material.SetBuffer("_InstanceColorBuffer" + id, colorBuffer);

            var rparams = new RenderParams(material) { matProps = matProps };

            int step = activeParticleCount / maxInstancesPerDraw;
            int remaining = activeParticleCount % maxInstancesPerDraw;

            for (int i = 0; i < step; i++)
            {
                matProps.SetInteger("_InstanceIDOffset" + id, offsetID);
                Graphics.RenderMeshInstanced(rparams, mesh, 0, matrices, maxInstancesPerDraw, offsetID);

                offsetID += maxInstancesPerDraw;
            }

            if (remaining > 0)
            {
                matProps.SetInteger("_InstanceIDOffset" + id, offsetID);
                Graphics.RenderMeshInstanced(rparams, mesh, 0, matrices, remaining, offsetID);
            }
        }

        void OnDestroy()
        {
            if (colorBuffer != null)
            {
                colorBuffer.Release();
                colorBuffer = null;
            }
        }
    }

}