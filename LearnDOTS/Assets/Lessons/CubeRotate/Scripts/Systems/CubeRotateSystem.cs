using Lesson2.Scripts.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Lesson2.Scripts.Systems
{
    [BurstCompile][UpdateInGroup(typeof(CubeRotateSystemGroup))]
    partial struct CubeRotateSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            //foreach (var (transform,rotateSpeed) in SystemAPI.Query<RefRW<LocalTransform>,RefRO<RotateSpeed>>())
            //{
            //    transform.ValueRW = transform.ValueRO.RotateY(rotateSpeed.ValueRO.Speed * deltaTime);
            //}

            var rotateJob = new RotateCubeWithJobEntity()
            {
                deltaTime = deltaTime
            };
            rotateJob.ScheduleParallel();
            //rotateJob.Schedule();
            //rotateJob.Run();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        
        }
    }
}
