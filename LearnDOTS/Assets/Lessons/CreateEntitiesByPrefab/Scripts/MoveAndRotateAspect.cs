using Lesson2.Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using RotateSpeed = Lessons.CubesWithJob.Scripts.Components.RotateSpeed;

public readonly partial struct MoveAndRotateAspect : IAspect
{
    private readonly RefRW<LocalTransform> LocalTransform;
    private readonly RefRO<RotateSpeed> RotateSpeed;

    public void SetPosition(float3 position)
    {
        LocalTransform.ValueRW.Position = position;
    }

    public void Rotate(float elapsedTime)
    {
        LocalTransform.ValueRW = LocalTransform.ValueRO.RotateY(elapsedTime *3);
    }
}
