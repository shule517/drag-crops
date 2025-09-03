using dragcrops.lib.attributes;
using Godot;

[AutoLoad]
public partial class Audio : Node
{
    private static Audio Instance { get; } = SceneTree.Root.GetNode<Audio>("/root/AutoLoad/Audio");
    private static SceneTree SceneTree => (SceneTree)Engine.GetMainLoop();

    // TODO: ↓をするとAudioSoundを勝手に取得してくれるイメージ。Treeから型で検索する
    // TODO: Provide()をするとTreeを辿らずに、明示的に参照を指定できる
    // [Dependency] Audio Audio { get; } = null!;
    // Audio.PlaySound(audioStream)

    // TODO: staticクラスからも取りたいよね
    // Audio.Instance.PlaySound(audioStream)
    // private static Audio Instance { get; } = SceneTree.Root.GetNode<Audio>("/root/AutoLoad/Audio");
    // private static SceneTree SceneTree => (SceneTree)Engine.GetMainLoop();

    // TODO: ↑どっちもTreeから検索してるから一緒では？

    public static void PlaySound(AudioStream audioStream, double pitchScale = 1.0f, float volumeDb = 0.0f)
    {
        var audioPlayer = new AudioStreamPlayer2D();
        audioPlayer.Stream = audioStream;
        audioPlayer.PitchScale = (float)pitchScale;
        audioPlayer.VolumeDb = volumeDb;
        audioPlayer.Autoplay = true;
        Instance.GetTree().CurrentScene.AddChild(audioPlayer);
        // player.global_position = node.global_position # 再生位置を設定

        // 再生終了後に削除する
        audioPlayer.Finished += () => audioPlayer.QueueFree();
    }
}
