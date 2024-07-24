using Lessons.WaveCubeWithEntities.Scripts.Aspects;
using Unity.Burst;
using Unity.Entities;

namespace Lessons.WaveCubeWithEntities.Scripts.Jobs
{
    [BurstCompile]
    public partial struct RotateAndMoveJob : IJobEntity
    {
        public float DeltaTime;
        public float ElapsedTime;
        public void Execute(RotateAndMoveAspect aspect)
        {
            aspect.RotateAndMove(ElapsedTime, DeltaTime);
        }
    }
}
