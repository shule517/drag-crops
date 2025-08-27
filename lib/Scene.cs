using System;
using Godot;

namespace dragcrops.lib;

public class Scene<TNode>(PackedScene packedScene) where TNode : Node2D
{
    private readonly PackedScene _packedScene = packedScene;

    // シーンの読み込み
    public static Scene<TNode> Load(string scenePath)
    {
        PackedScene packedScene = GD.Load<PackedScene>(scenePath);
        if (packedScene == null)
        {
            // TODO: 起動時にエラーを投げたい
            throw new InvalidOperationException($"シーンのパスが見つかりませんでした: {scenePath}");
        }

        return new Scene<TNode>(packedScene);
    }

    // シーンを生成する
    public TNode Instantiate()
    {
        return _packedScene.Instantiate<TNode>();
    }
}
