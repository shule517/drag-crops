using System;
using Godot;

public static class AnimatedSprite2DExtension
{
    // 例: animatedSprite2D.SetShaderParameter("is_selected", isSelected);
    public static ShaderMaterial? ShaderMaterial(this AnimatedSprite2D me) => me.Material as ShaderMaterial;

    public static void SetShaderParameter(this AnimatedSprite2D me, StringName param, Variant value)
    {
        var shaderMaterial = me.ShaderMaterial();
        if (shaderMaterial == null)
            throw new InvalidOperationException("shaderMaterialが取得できない");
        shaderMaterial.SetShaderParameter(param, value);
    }
}
