using Lessons.GenerateAndDestroyCubeWithEcb.Scripts.AuthoringAndComponents;
using Lessons.WaveCubeWithEntities.Scripts.AuthoringAndComponents;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

namespace Lessons.GenerateAndDestroyCubeWithEcb.Scripts.Jobs
{
    [BurstCompile]
    public struct GenerateJob : IJobFor
    {
        [ReadOnly] public Entity CubePrototype;
        public NativeArray<Entity> Cubes;
        public EntityCommandBuffer.ParallelWriter ParallelWriter;
        [NativeDisableUnsafePtrRestriction]public RefRW<RandomComponent> RandomComponent;
        public void Execute(int index)
        {
            Cubes[index] = ParallelWriter.Instantiate(index, CubePrototype);
            ParallelWriter.AddComponent<Life>(index, Cubes[index], new Life
            {
                TotalLifeTime = 10,
                CurrentLifeTime = 0
            });
            ParallelWriter.AddComponent<RotateAndMoveSpeed>(index,Cubes[index],new RotateAndMoveSpeed
            {
                MoveSpeed = 1,
                RotateSpeed = 1,
            });
            var targetPos = RandomComponent.ValueRW.Random.NextFloat2(new float2(-15, -15), new float2(15, 15));
            ParallelWriter.AddComponent<TargetPos>(index, Cubes[index], new TargetPos
            {
                TargetPosValue = new float3(targetPos.x, 0, targetPos.y)
            });
        }
    }
}
