using Lessons.GenerateAndDestroyCubeWithEcb.Scripts.AuthoringAndComponents;
using Unity.Entities;

namespace Lessons.GenerateAndDestroyCubeWithEcb.Scripts.Jobs
{
    public partial struct DestroyJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ParallelWriter;
        public float DeltaTime;
        void Execute([ChunkIndexInQuery] int chunkIndex,Entity entity, ref Life life)
        {
            life.CurrentLifeTime += DeltaTime;
            if (life.CurrentLifeTime >= life.TotalLifeTime)
            {
                ParallelWriter.DestroyEntity(chunkIndex,entity);
            }
        }
    }
}
