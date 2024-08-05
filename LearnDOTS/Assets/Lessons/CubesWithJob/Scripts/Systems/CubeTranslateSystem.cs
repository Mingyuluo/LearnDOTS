using Lessons.CubesWithJob.Scripts.Components;
using Lessons.CubesWithJob.Scripts.Jobs;
using Lessons.GenerateAndDestroyCubeWithEcb.Scripts.AuthoringAndComponents;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Lessons.CubesWithJob.Scripts.Systems
{
    [BurstCompile]
    partial struct CubeTranslateSystem : ISystem
    {
        private EntityQuery _cubeQuery;
        private ComponentLookup<RotateSpeed> _rotateSpeedLookUp;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            _rotateSpeedLookUp = state.GetComponentLookup<RotateSpeed>();
            state.RequireForUpdate<CubeGeneratorParam>();
            state.RequireForUpdate<BeginPresentationEntityCommandBufferSystem.Singleton>();
            var queryBuilder = new EntityQueryBuilder(Allocator.Temp).WithAll<RotateSpeed, LocalTransform>()
                .WithOptions(EntityQueryOptions.IgnoreComponentEnabledState);
            _cubeQuery = state.GetEntityQuery(queryBuilder);
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.TempJob);
            var ecbWriter = ecb.AsParallelWriter();
            var job1 = new TranslateAndStopCubeJob()
            {
                Deltatime = SystemAPI.Time.DeltaTime,
                ECBWriter = ecbWriter
            };
            state.Dependency = job1.ScheduleParallel(state.Dependency);
            state.Dependency.Complete();
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
            
            _rotateSpeedLookUp.Update(ref state);
            var generator = SystemAPI.GetSingleton<CubeGeneratorParam>();
            var job0 = new StopRotateJob()
            {
                DeltaTime = SystemAPI.Time.DeltaTime,
                ElapsedTime = (float)SystemAPI.Time.ElapsedTime,
                LeftRightBound = new float2(generator.GeneratorAreaPos.x / 2, generator.TargetAreaPos.x / 2),
                RotateSpeedLookUp = _rotateSpeedLookUp,
            };
            state.Dependency = job0.ScheduleParallel(_cubeQuery, state.Dependency);
            state.Dependency.Complete();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        
        }
    }
}
