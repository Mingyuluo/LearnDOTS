using Lesson2.Scripts.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
[RequireMatchingQueriesForUpdate]
partial struct CubeGeneratorByPrefabSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<CubeGeneratorByPrefab>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var creator = SystemAPI.GetSingleton<CubeGeneratorByPrefab>();
        var cubes = CollectionHelper.CreateNativeArray<Entity>(creator.CubeCount, Allocator.Temp);
        state.EntityManager.Instantiate(creator.CubePrototype, cubes);
        int count = 0;
        foreach (var cube in cubes)
        {
            state.EntityManager.AddComponentData<RotateSpeed>(cube, new RotateSpeed
            {
                Speed = count * math.radians(60)
            });
            var position = new float3((count - creator.CubeCount * 0.5f) * 1.2f, 0, 0);
            var transform = SystemAPI.GetAspect<MoveAndRotateAspect>(cube);
            transform.SetPosition(position);
            count++;
        }

        cubes.Dispose();
        state.Enabled = false;
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}

