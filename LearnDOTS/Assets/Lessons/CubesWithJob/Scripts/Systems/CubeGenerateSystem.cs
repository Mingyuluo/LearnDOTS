using Lessons.CubesWithJob.Scripts.Components;
using Lessons.GenerateAndDestroyCubeWithEcb.Scripts.AuthoringAndComponents;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Lessons.CubesWithJob.Scripts.Systems
{
    partial struct CubeGenerateSystem : ISystem
    {
        private int _totalCount;
        private float _time;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            _time = 0;
            _totalCount = 0;
            state.RequireForUpdate<CubeGeneratorParam>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var cubeGenerator = SystemAPI.GetSingleton<CubeGeneratorParam>();
            if (_totalCount >= cubeGenerator.TotalCount)
            {
                state.Enabled = false;
            }
            else
            {
                _time += SystemAPI.Time.DeltaTime;
                if (_time >= cubeGenerator.GenerateInterval)
                {
                    GenerateCube(ref state,cubeGenerator);
                    _time -= cubeGenerator.GenerateInterval;
                    _totalCount += cubeGenerator.GenerateCountEachTime;
                }
            }
        }

        private void GenerateCube(ref SystemState state,CubeGeneratorParam cubeGenerator)
        {

            var cubes = CollectionHelper.CreateNativeArray<Entity>(cubeGenerator.GenerateCountEachTime,Allocator.Temp);
            state.EntityManager.Instantiate(cubeGenerator.CubePrototype, cubes);
            foreach (var cube in cubes)
            {
                state.EntityManager.AddComponentData(cube, new RotateSpeed
                {
                    Speed = cubeGenerator.RotateSpeed
                });
                state.EntityManager.AddComponentData(cube, new MoveSpeed
                {
                    Speed = cubeGenerator.MoveSpeed
                });
                var random = SystemAPI.GetSingletonRW<RandomComponent>();
                var randPos = random.ValueRW.Random.NextFloat3(-cubeGenerator.TargetAreaSize * 0.5f,
                    cubeGenerator.TargetAreaSize * 0.5f);
                state.EntityManager.AddComponentData(cube, new RandomTargetPos
                {
                    TargetPos = cubeGenerator.TargetAreaPos + randPos
                });
                random = SystemAPI.GetSingletonRW<RandomComponent>();
                randPos = random.ValueRW.Random.NextFloat3(-cubeGenerator.GeneratorAreaSize * 0.5f,
                    cubeGenerator.GeneratorAreaSize * 0.5f);
                var position = randPos + cubeGenerator.GeneratorAreaPos;
                var transform = SystemAPI.GetComponentRW<LocalTransform>(cube);
                transform.ValueRW.Position = position;
            }
            cubes.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        
        }

    }
}
