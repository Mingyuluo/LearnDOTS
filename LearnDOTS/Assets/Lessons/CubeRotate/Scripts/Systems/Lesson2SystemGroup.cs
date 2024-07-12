using Unity.Entities;
using UnityEngine;

public partial class Lesson2SystemGroup : ComponentSystemGroup { }

[UpdateInGroup(typeof(Lesson2SystemGroup))]
public partial class CubeRotateSystemGroup : SceneSystemGroup
{
    protected override string SceneName => "RotateCubeAuthoring";
}
