using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lessons.CubesWithJob.Scripts.Components
{
    public struct CubeGeneratorParam : IComponentData
    {
        public Entity CubePrototype;
        public int TotalCount;
        public float GenerateInterval;
        public int GenerateCountEachTime;
        public float MoveSpeed;
        public float RotateSpeed;
        public float3 GeneratorAreaPos;
        public float3 GeneratorAreaSize;
        public float3 TargetAreaPos;
        public float3 TargetAreaSize;
    }

    public class CubeGeneratorParamAuthoring : MonoBehaviour
    {
        public GameObject cubePrototype;
        [Range(1, 10000)] public int totalCount = 10000;
        [Range(0.1f, 1.0f)] public float generateInterval = 0.2f;
        [Range(1, 10)] public int generateCountEachTime = 5;
        public float moveSpeed = 5;
        public float rotateSpeed = 180;
        public float3 generatorAreaPos;
        public float3 generatorAreaSize;
        public float3 targetAreaPos;
        public float3 targetAreaSize;

        public class CubeGeneratorParamBaker : Baker<CubeGeneratorParamAuthoring>
        {
            public override void Bake(CubeGeneratorParamAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,
                    new CubeGeneratorParam
                    {
                        CubePrototype = GetEntity(authoring.cubePrototype, TransformUsageFlags.Dynamic),
                        TotalCount = authoring.totalCount,
                        GenerateInterval = authoring.generateInterval,
                        GenerateCountEachTime = authoring.generateCountEachTime,
                        MoveSpeed = authoring.moveSpeed,
                        RotateSpeed = authoring.rotateSpeed,
                        GeneratorAreaPos = authoring.generatorAreaPos,
                        GeneratorAreaSize = authoring.generatorAreaSize,
                        TargetAreaPos = authoring.targetAreaPos,
                        TargetAreaSize = authoring.targetAreaSize
                    });
            }
        }
    }
}
