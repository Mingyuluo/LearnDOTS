using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lessons.GenerateAndDestroyCubeWithEcb.Scripts.AuthoringAndComponents
{
    public struct TargetPos : IComponentData
    {
        public float3 TargetPosValue;
    }

    public class TargetPosAuthoring : MonoBehaviour
    {
        public float3 targetPosValue;

        public class TargetPosBaker : Baker<TargetPosAuthoring>
        {
            public override void Bake(TargetPosAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new TargetPos { TargetPosValue = authoring.targetPosValue });
            }
        }
    }
}
