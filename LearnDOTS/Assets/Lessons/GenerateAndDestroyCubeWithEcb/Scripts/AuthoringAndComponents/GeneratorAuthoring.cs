using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lessons.GenerateAndDestroyCubeWithEcb.Scripts.AuthoringAndComponents
{
    public struct Generator : IComponentData
    {
        public int GenerateCount;
        public float GenerateInterval;
        public Entity CubePrototype;
    }

    public class GeneratorAuthoring : MonoBehaviour
    {
        [FormerlySerializedAs("generatePerFrame")] public int generateCount;
        public float generateInterval;
        public GameObject cubePrototype;

        public class GeneratorBaker : Baker<GeneratorAuthoring>
        {
            public override void Bake(GeneratorAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,
                    new Generator
                    {
                        GenerateCount = authoring.generateCount,
                        GenerateInterval = authoring.generateInterval,
                        CubePrototype = GetEntity(authoring.cubePrototype, TransformUsageFlags.Dynamic)
                    });
            }
        }
    }
}
