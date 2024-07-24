using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lessons.WaveCubeWithEntities.Scripts.AuthoringAndComponents
{
    public struct CubeGeneratorParams : IComponentData
    {
        public int XHalfCount;
        public int ZHalfCount;
        public Entity CubePrototype;
    }

    public class CubeGeneratorParamsAuthoring : MonoBehaviour
    {
        public int xHalfCount;
        public int zHalfCount;
        public GameObject cubePrototype;

        public class CubeGeneratorBaker : Baker<CubeGeneratorParamsAuthoring>
        {
            public override void Bake(CubeGeneratorParamsAuthoring paramsAuthoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,
                    new CubeGeneratorParams
                    {
                        XHalfCount = paramsAuthoring.xHalfCount,
                        ZHalfCount = paramsAuthoring.zHalfCount,
                        CubePrototype = GetEntity(paramsAuthoring.cubePrototype, TransformUsageFlags.Dynamic)
                    });
            }
        }
    }
}
