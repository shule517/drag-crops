using Godot;
using System;

public partial class Audio : Node
{
    static public SceneTree Tree => Engine.GetMainLoop() as SceneTree;
    static public Audio Instance => Tree.Root.GetNode<Audio>("/root/Audio");

    public void PlaySound(AudioStream audioStream, double pitchScale = 1.0f)
    {
        var audioPlayer = new AudioStreamPlayer2D();
        audioPlayer.Stream = audioStream;
        audioPlayer.PitchScale = (float)pitchScale;
        audioPlayer.Autoplay = true;
        GetTree().CurrentScene.AddChild(audioPlayer);

        // var player := AudioStreamPlayer2D.new()
        // player.stream = sound_effect
        // player.pitch_scale = pitch_scale
        // player.volume_db = volume_db
        // player.global_position = node.global_position # 再生位置を設定
        // player.autoplay = true
        // get_tree().current_scene.add_child(player)
        //
        // # 再生終了後に削除
        // player.finished.connect(player.queue_free)
    }
}
