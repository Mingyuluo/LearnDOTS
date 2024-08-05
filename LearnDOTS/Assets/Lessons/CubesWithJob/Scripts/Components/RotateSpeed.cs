using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lessons.CubesWithJob.Scripts.Components
{
    public struct RotateSpeed : IComponentData,IEnableableComponent
    {
        public float Speed;
    }
}
