using UnityEngine;

namespace Sensen.Components
{
    public class MusicService : AudioPlayerBase<MusicService>
    {
        [SerializeField] private AudioProfile _bootProfile;

        protected override void AwakeSingleton()
        {
            base.AwakeSingleton();
            if (_bootProfile != null)
            {
                Play(_bootProfile);
            }
        }
    }
}
