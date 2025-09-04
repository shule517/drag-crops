namespace dragcrops.lib;
using System;
using Godot;

public class Scene<TNode>(PackedScene packedScene) where TNode : Node2D
{
    // シーンの読み込み
    public static Scene<TNode> Load(string scenePath)
    {
        PackedScene packedScene = GD.Load<PackedScene>(scenePath);
        if (packedScene == null)
        {
            // TODO: 起動時にエラーを投げたい
            throw new InvalidOperationException($"シーンが見つかりませんでした: {scenePath}");
        }

        return new Scene<TNode>(packedScene);
    }

    // シーンを生成する
    public TNode Instantiate()
    {
        return packedScene.Instantiate<TNode>();
    }
}
