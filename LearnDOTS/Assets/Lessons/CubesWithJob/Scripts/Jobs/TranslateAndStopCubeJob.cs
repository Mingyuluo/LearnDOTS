using Lessons.CubesWithJob.Scripts.Aspects;
using Unity.Entities;

namespace Lessons.CubesWithJob.Scripts.Jobs
{
    public partial struct TranslateAndStopCubeJob : IJobEntity
    {
        public float Deltatime;
        public EntityCommandBuffer.ParallelWriter ECBWriter;
        public void Execute([ChunkIndexInQuery]int chunkIndex, Entity entity, CubeTranslationAspect aspect)
        {
            if (aspect.IsNeedDestroy())
            {
                ECBWriter.DestroyEntity(chunkIndex, entity);
            }
            else
            {
                aspect.MoveCube(Deltatime);
            }
        }
    }
}
