using UnityEngine;
using UniRx;

namespace yayu.DOTS
{
    public interface ICircleDataAccessor
    {
        bool IsActive();
        Vector2 Position { get; }
        float Radius { get; }

        void SetRadiusLerpTime(float t);
    }

    public class ParticleCropperData : ICircleDataAccessor
    {
        public bool IsActive() => isActive;
        public Vector2 Position => position;
        public float Radius => radius;
        public void SetRadiusLerpTime(float t) { }

        bool isActive;
        Vector2 position;
        float radius;

        public ParticleCropperData()
        {
            isActive = false;
            position = Vector2.zero;
            radius = 0f;
        }

        public void SetActive(bool isActive)
        {
            this.isActive = isActive;
            if (isActive == false) position = new Vector2(-4000, -4000);
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        public void SetRadius(float radius)
        {
            this.radius = radius;
        }

    }

    public class SingleCircleMeshRenderer : MonoBehaviour
    {
        private ICircleDataAccessor circleData;
        [SerializeField] float scaleMultiplier = 1f;
        [SerializeField] float duration = 0.6f;
        [SerializeField, Range(0.0f, 1.0f)] float maxAlpha = 0.3f;
        SpriteRenderer sprite;
        bool isActive;
        float radius;
        float alpha;
        float maxRadius => circleData == null ? 0 : circleData.Radius * scaleMultiplier;

        public void Init(ICircleDataAccessor dataAccessor)
        {
            circleData = dataAccessor;

            sprite = GetComponentInChildren<SpriteRenderer>();

            // IsActive‚Ì•ÏX‚ðw“Ç
            Observable.EveryUpdate()
                .Select(_ => circleData.IsActive())
                .DistinctUntilChanged()
                .Where(_ => _ == true)
                .Subscribe(isActive =>
                {
                    this.isActive = isActive;
                    this.radius = maxRadius;
                    this.alpha = 1f;
                })
                .AddTo(this);

            // position‚Ì•ÏX‚ðw“Ç
            Observable.EveryUpdate()
                .Where(_ => circleData.IsActive())
                .Select(_ => circleData.Position)
                .DistinctUntilChanged()
                .Subscribe(pos =>
                {
                    transform.position = new Vector3(pos.x, pos.y, 0);
                })
                .AddTo(this);
        }

        private void Update()
        {
            if (!isActive) return;

            alpha -= Time.deltaTime / duration;
            radius = MyMath.Remap(alpha, 0, 1, 0.7f, 1) * maxRadius;

            if (alpha < 0f)
            {
                alpha = 0f;
                isActive = false;
            }

            var col = sprite.color;
            col.a = MyMath.Remap(alpha, 0, 1, 0f, maxAlpha);
            sprite.color = col;
            transform.localScale = Vector3.one * radius;
        }
    }
}