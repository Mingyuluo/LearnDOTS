using Lesson2.Scripts.Components;
using Unity.Entities;
using Unity.Transforms;

public partial struct RotateCubeWithJobEntity : IJobEntity
{
    public float deltaTime;
    public void Execute(ref LocalTransform transform,in RotateSpeed speed)
    {
        transform = transform.RotateY(speed.Speed * deltaTime);
    }
}
