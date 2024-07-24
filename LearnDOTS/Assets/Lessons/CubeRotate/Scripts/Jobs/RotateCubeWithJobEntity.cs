using Unity.Burst;
using Unity.Entities;

[BurstCompile]
partial struct RotateCubeWithJobEntity : IJobEntity
{
    public float deltaTime;
    void Execute(MoveAndRotateAspect aspect)
    { 
        aspect.Rotate(deltaTime);
    }
}
