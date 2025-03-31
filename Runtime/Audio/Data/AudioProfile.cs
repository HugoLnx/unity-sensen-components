using SensenToolkit;
using UnityEngine;

namespace Sensen.Components
{
    [CreateAssetMenu(fileName = "AudioProfile", menuName = "Sensen/Audio/Profile", order = 1)]
    public class AudioProfile : ScriptableObject
    {
        [field: SerializeField]
        public AudioClip[] Clips { get; private set; }
        [field: SerializeField]
        public bool RandomizeClips { get; private set; } = true;
        [field: SerializeField]
        public AudioTrack Track { get; private set; }
        [field: SerializeField, Range(0f, 2f)]
        public float Volume { get; private set; } = 1f;

        [field: SerializeField]
        public AudioPlaybackProfileBase PlaybackProfile { get; private set; }

        private RandomWithVariability _random;
        private RandomWithVariability VarRandom => _random ??= new(
            optionsAmount: Clips.Length,
            percentReductionOnSelect: 0.25f,
            noSequentialRepetition: true
        );

        private int _clipIndex = 0;

        public AudioPlaybackCommand GetCommand(AudioTrack track = null, float volumeModifier = 1f)
        {
            if (track == null) track = Track;
            if (track == null) track = AudioTrack.Global;
            return new AudioPlaybackCommand(
                clip: ChooseClip(),
                volume: PlaybackProfile.Volume * Volume * volumeModifier,
                loop: PlaybackProfile.Loop,
                pitch: PlaybackProfile.ChoosePitch(),
                track: track
            );
        }

        private AudioClip ChooseClip()
        {
            if (Clips.Length == 0) return null;
            if (RandomizeClips) return Clips[VarRandom.Select()];
            AudioClip clip = Clips[_clipIndex];
            _clipIndex = (_clipIndex + 1) % Clips.Length;
            return clip;
        }
    }
}
