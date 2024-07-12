using Unity.Entities;
using Unity.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
public abstract partial class SceneSystemGroup : ComponentSystemGroup
{
    private bool _initialized;

    protected virtual string SceneName { get; }
    
    protected override void OnCreate()
    {
        base.OnCreate();
        _initialized = false;
    }

    protected override void OnUpdate()
    {
        if (!_initialized)
        {
            if (SceneManager.GetActiveScene().isLoaded)
            {
                var subScene = Object.FindFirstObjectByType<SubScene>();
                if (subScene != null)
                {
                    Enabled = SceneName == subScene.gameObject.scene.name;
                }
                else
                {
                    Enabled = false;
                }

                _initialized = true;
            }
        }

        base.OnUpdate();
    }


}
