using Lessons.WaveCubeWithEntities.Scripts.Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace Lessons.WaveCubeWithEntities.Scripts.Jobs
{
    [BurstCompile]
    public partial struct SetPositionJob : IJobEntity
    {
        public int XCount;
        void Execute([EntityIndexInQuery]int index,RotateAndMoveAspect aspect)
        {
            var x = index / XCount;
            var z = index % XCount;
            var position = new float3(x * 1.1f, 0, z * 1.1f);
            aspect.SetPosition(position);
        }
    }
}
