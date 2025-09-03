using dragcrops.lib.attributes;
using Godot;

[AutoLoad]
public partial class Audio : Node
{
    public void Play(AudioStream audioStream, double pitchScale = 1.0f, float volumeDb = 0.0f)
    {
        var audioPlayer = new AudioStreamPlayer2D();
        audioPlayer.Stream = audioStream;
        audioPlayer.PitchScale = (float)pitchScale;
        audioPlayer.VolumeDb = volumeDb;
        audioPlayer.Autoplay = true;
        GetTree().CurrentScene.AddChild(audioPlayer);
        // player.global_position = node.global_position # 再生位置を設定

        // 再生終了後に削除する
        audioPlayer.Finished += () => audioPlayer.QueueFree();
    }

    // private static Audio Instance { get; } = SceneTree.Root.GetNode<Audio>("/root/AutoLoad/Audio");
    // private static SceneTree SceneTree => (SceneTree)Engine.GetMainLoop();

    // TODO: ↓をするとAudioSoundを勝手に取得してくれるイメージ。Treeから型で検索する
    // TODO: Provide()をするとTreeを辿らずに、明示的に参照を指定できる
    // [Dependency] Audio Audio { get; } = null!;
    // Audio.PlaySound(audioStream)
    // _Readyで[Dependency]経由でAudioがセットされる
    // Audioはメンバー変数なので、変なところから呼ばれることはない
    // _Readyより前に実行されると落ちる

    // TODO: staticクラスからも取りたいよね
    // Audio.Instance.PlaySound(audioStream)
    // private static Audio Instance { get; } = SceneTree.Root.GetNode<Audio>("/root/AutoLoad/Audio");
    // private static SceneTree SceneTree => (SceneTree)Engine.GetMainLoop();
    // Audio.Instanceが_Ready以降に呼ばれれば問題ない
    // _Readyより前に実行されると落ちる

    // TODO: ↑どっちもTreeから検索してるから一緒では？
}
