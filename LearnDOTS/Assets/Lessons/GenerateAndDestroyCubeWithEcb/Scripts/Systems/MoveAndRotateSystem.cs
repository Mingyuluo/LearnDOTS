using Unity.Burst;
using Unity.Entities;

namespace Lessons.GenerateAndDestroyCubeWithEcb.Scripts.Systems
{
    [BurstCompile]
    partial struct MoveAndRotateSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var jobHandle = new MoveAndRotateJob
            {
                ElapsedTime = (float)SystemAPI.Time.ElapsedTime,
                DeltaTime = SystemAPI.Time.DeltaTime
            };
            jobHandle.ScheduleParallel();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        
        }
    }
}
