using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] //  so we can preview image effect in editor
public class CameraImageEffect : MonoBehaviour
{
    public Material EffectMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //  Blit = render this source, to that destination, passing through this material
        Graphics.Blit(source, destination, EffectMaterial);
    }
}
