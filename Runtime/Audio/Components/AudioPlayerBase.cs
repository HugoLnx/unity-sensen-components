using System;
using MyBox;
using SensenToolkit;
using UnityEngine;

namespace Sensen.Components
{
    public abstract class AudioPlayerBase<T> : APermanentSingleton<T>
    where T : APermanentSingleton<T>
    {
        [SerializeField, Range(0f, 1f)] private float _globalVolume = 1f;
        [SerializeField] private bool _isMuted;
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private AudioOutputPool _outputPool;

        public void Play(AudioProfile profile, AudioTrack track = null, Action onFinished = null)
        {
            Play(profile.GetCommand(track: track), onFinished);
        }

        public void Play(AudioPlaybackCommand command, Action onFinished = null)
        {
            AudioOutput output = command.Loop || command.UseGlobalTrack ? _outputPool.Get() : _outputPool.GetReusable(command.Pitch);
            if (output == null)
            {
                Debug.LogWarning($"[{nameof(T)}] No audio output available. Abort playing {command.Clip.name}");
                return;
            }
            output.UpdateVolume(modifier: _globalVolume);
            output.UpdateTrack(command.Track);
            output.Play(command, onFinished);
        }
        public void SetIsAudible(bool isAudible)
        {
            _isMuted = !isAudible;
            UpdateAllAudioOutputs();
        }

        public void SetGlobalVolume(float volume)
        {
            _globalVolume = volume;
            UpdateAllAudioOutputs();
        }

        private void UpdateAllAudioOutputs()
        {
            foreach (AudioOutput output in _outputPool.Creations)
            {
                output.UpdateVolume(modifier: _globalVolume);
                output.UpdateMute(isMute: _isMuted);
            }
        }
    }
}
