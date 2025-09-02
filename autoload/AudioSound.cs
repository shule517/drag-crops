using dragcrops.lib.attributes;
using Godot;

[AutoLoad]
public partial class AudioSound : Node
{
    // public static void PlaySound(AudioStream audioStream, double pitchScale = 1.0f, float volumeDb = 0.0f)
    // {
    //     var audioPlayer = new AudioStreamPlayer2D();
    //     audioPlayer.Stream = audioStream;
    //     audioPlayer.PitchScale = (float)pitchScale;
    //     audioPlayer.VolumeDb = volumeDb;
    //     audioPlayer.Autoplay = true;
    //     Instance.GetTree().CurrentScene.AddChild(audioPlayer);
    //     // player.global_position = node.global_position # 再生位置を設定
    //
    //     // 再生終了後に削除する
    //     audioPlayer.Finished += () => audioPlayer.QueueFree();
    // }
}
