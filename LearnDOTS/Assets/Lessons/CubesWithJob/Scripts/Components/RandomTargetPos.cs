using Unity.Entities;
using Unity.Mathematics;

namespace Lessons.CubesWithJob.Scripts.Components
{
    public struct RandomTargetPos : IComponentData
    {
        public float3 TargetPos;
    }
}
