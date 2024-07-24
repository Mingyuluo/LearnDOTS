using Lessons.GenerateAndDestroyCubeWithEcb.Scripts.AuthoringAndComponents;
using Lessons.GenerateAndDestroyCubeWithEcb.Scripts.Jobs;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace Lessons.GenerateAndDestroyCubeWithEcb.Scripts.Systems
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    partial struct GenerateSystem : ISystem
    {
        private float _timer;
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginPresentationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<RandomComponent>();
            state.RequireForUpdate<Generator>();
            _timer = 0;
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var generator = SystemAPI.GetSingleton<Generator>();
            if (_timer > generator.GenerateInterval)
            {
                CreateGenerateCubeJob(ref state,generator);
                _timer -= generator.GenerateInterval;
            }
            Debug.Log($"***** {_timer}");
            _timer += Time.deltaTime;
        }

        private void CreateGenerateCubeJob(ref SystemState state,Generator generator)
        {
            var random = SystemAPI.GetSingletonRW<RandomComponent>();
            var ecbSingleton = SystemAPI.GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            var ecbWriter = ecb.AsParallelWriter();
            var cubes = CollectionHelper.CreateNativeArray<Entity>(generator.GenerateCount, Allocator.TempJob);
            var job = new GenerateJob
            {
                RandomComponent = random,
                CubePrototype = generator.CubePrototype,
                Cubes = cubes,
                ParallelWriter = ecbWriter
            };
            state.Dependency = job.ScheduleParallel(generator.GenerateCount, 1, state.Dependency);
            state.Dependency.Complete();
            cubes.Dispose();
        }
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        
        }
    }
}
