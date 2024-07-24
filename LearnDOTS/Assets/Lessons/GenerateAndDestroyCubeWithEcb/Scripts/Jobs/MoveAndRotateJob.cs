using Lessons.WaveCubeWithEntities.Scripts.Aspects;
using Unity.Entities;
using Unity.Transforms;

public partial struct MoveAndRotateJob : IJobEntity
{
    public float ElapsedTime;
    public float DeltaTime;

    private void Execute(RotateAndMoveAspect aspect)
    {
        aspect.RotateAndMove(ElapsedTime, DeltaTime);
    }
}
