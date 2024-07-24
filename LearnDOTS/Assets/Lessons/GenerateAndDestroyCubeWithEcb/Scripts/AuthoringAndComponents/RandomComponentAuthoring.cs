using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;
using Random = Unity.Mathematics.Random;

namespace Lessons.GenerateAndDestroyCubeWithEcb.Scripts.AuthoringAndComponents
{
    public struct RandomComponent : IComponentData
    {
        public Random Random;
    }

    public class RandomComponentAuthoring : MonoBehaviour
    {
        public uint random;

        public class RandomComponentBaker : Baker<RandomComponentAuthoring>
        {
            public override void Bake(RandomComponentAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,
                    new RandomComponent { Random = Unity.Mathematics.Random.CreateFromIndex(authoring.random) });
            }
        }
    }
}
