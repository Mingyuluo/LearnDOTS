using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lessons.GenerateAndDestroyCubeWithEcb.Scripts.AuthoringAndComponents
{
    public struct Life : IComponentData
    {
        public float TotalLifeTime;
        public float CurrentLifeTime;
    }

    public class LifeAuthoring : MonoBehaviour
    {
        public float lifeTime;
        public float currentLifeTime;

        public class LifeBaker : Baker<LifeAuthoring>
        {
            public override void Bake(LifeAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,
                    new Life { TotalLifeTime = authoring.lifeTime, CurrentLifeTime = authoring.currentLifeTime });
            }
        }
    }
}
