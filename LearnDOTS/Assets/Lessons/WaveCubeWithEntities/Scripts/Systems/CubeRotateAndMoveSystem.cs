using Lessons.WaveCubeWithEntities.Scripts.Jobs;
using Unity.Burst;
using Unity.Entities;
using Unity.Profiling;

namespace Lessons.WaveCubeWithEntities.Scripts.Systems
{
    [BurstCompile]
    partial struct CubeRotateAndMoveSystem : ISystem
    {
        private static readonly ProfilerMarker ProfilerMarker =
            new ProfilerMarker("WaveCubeWithEntities UpdateTransform");
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            using (ProfilerMarker.Auto())
            {
                var jobHandle = new RotateAndMoveJob()
                {
                    ElapsedTime =  (float)SystemAPI.Time.ElapsedTime,
                    DeltaTime = SystemAPI.Time.fixedDeltaTime
                };
                jobHandle.ScheduleParallel();
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        
        }
    }
}
