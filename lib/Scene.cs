using Godot;

namespace dragcrops.lib;

public class Scene<TNode>(PackedScene packedScene) where TNode : Node2D
{
    private readonly PackedScene _packedScene = packedScene;
    public static Scene<TNode> Load(string scenePath)
    {
        PackedScene packedScene = GD.Load<PackedScene>(scenePath);
        return new Scene<TNode>(packedScene);
    }

    public TNode Instantiate()
    {
        return _packedScene.Instantiate<TNode>();
    }
}
