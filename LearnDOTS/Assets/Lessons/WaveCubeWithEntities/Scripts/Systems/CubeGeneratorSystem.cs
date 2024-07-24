using Lessons.WaveCubeWithEntities.Scripts.Aspects;
using Lessons.WaveCubeWithEntities.Scripts.AuthoringAndComponents;
using Lessons.WaveCubeWithEntities.Scripts.Jobs;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Lessons.WaveCubeWithEntities.Scripts.Systems
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    partial struct CubeGeneratorSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginPresentationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<CubeGeneratorParams>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var generator = SystemAPI.GetSingleton<CubeGeneratorParams>();
            var cubes = CollectionHelper.CreateNativeArray<Entity>(4 * generator.XHalfCount * generator.ZHalfCount,
                Allocator.Temp);
            state.EntityManager.Instantiate(generator.CubePrototype, cubes);
            
            for (int x = -generator.XHalfCount; x < generator.XHalfCount; x++)
            {
                for (int z = -generator.ZHalfCount; z < generator.ZHalfCount; z++)
                {
                    var index = (z + generator.ZHalfCount) * 2 * generator.XHalfCount + (x + generator.XHalfCount);
                    var rotateAndMoveAspect = SystemAPI.GetAspect<RotateAndMoveAspect>(cubes[index]);
                    rotateAndMoveAspect.SetPosition(new float3(x * 1.1f, 0, z * 1.1f));
                }
            }
            cubes.Dispose();
            var ecbSingleton = SystemAPI.GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            var ecbWriter = ecb.AsParallelWriter();
            
            state.Enabled = false;
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        
        }
    }
}
