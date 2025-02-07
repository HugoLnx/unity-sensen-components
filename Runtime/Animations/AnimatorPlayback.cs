using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SensenComponents
{
    public class AnimatorPlayback

    {
        private readonly Animator _animator;
        private readonly Func<AnimatorStateInfo, bool> _identifyState;
        private readonly int _layer;
        private List<AnimatorClipInfo> _clipsBuffer = new();

        public AnimatorPlayback(Animator animator, Func<AnimatorStateInfo, bool> identifyState, int layer = -1)
        {
            _identifyState = identifyState;
            _layer = FixLayer(layer);
            _animator = animator;
        }

        public static AnimatorPlayback BuildForHash(Animator animator, int hash, int layer = -1)
        {
            return new AnimatorPlayback(
                animator: animator,
                identifyState: state => state.fullPathHash == hash || state.shortNameHash == hash,
                layer: FixLayer(layer)
            );
        }

        public AnimatorPlaybackState GetCurrentState()
        {
            AnimatorStateInfo? currentState = IfValidState(_animator.GetCurrentAnimatorStateInfo(_layer));
            AnimatorStateInfo? nextState = IfValidState(_animator.GetNextAnimatorStateInfo(_layer));

            if (nextState.HasValue)
            {
                return new AnimatorPlaybackState(
                    state: nextState.Value,
                    clip: GetNextFirstClip(_layer),
                    isTransitioningIn: true
                );
            }
            else if (currentState.HasValue)
            {
                return new AnimatorPlaybackState(
                    state: currentState.Value,
                    clip: GetCurrentFirstClip(_layer),
                    isTransitioningOut: _animator.IsInTransition(_layer)
                );
            }
            else
            {
                return AnimatorPlaybackState.None;
            }
        }

        public IEnumerator WaitForCompletion(float delay = 0.15f)
        {
            WaitForSeconds wait = new(delay);
            while (GetCurrentState().IsPlaying)
            {
                yield return wait;
            }
        }

        private AnimationClip GetCurrentFirstClip(int? layer = null)
        {
            _clipsBuffer.Clear();
            _animator.GetCurrentAnimatorClipInfo(FixLayer(layer ?? _layer), _clipsBuffer);
            if (_clipsBuffer.Count == 0)
            {
                return null;
            }
            return _clipsBuffer[0].clip;
        }

        private AnimationClip GetNextFirstClip(int? layer = null)
        {
            _clipsBuffer.Clear();
            _animator.GetNextAnimatorClipInfo(FixLayer(layer ?? _layer), _clipsBuffer);
            if (_clipsBuffer.Count == 0)
            {
                return null;
            }
            return _clipsBuffer[0].clip;
        }

        private AnimatorStateInfo? IfValidState(AnimatorStateInfo state)
        {
            bool isBlankState = state.fullPathHash == 0;
            if (!isBlankState && _identifyState.Invoke(state))
            {
                return state;
            }
            return null;
        }

        private static int FixLayer(int layer) => Mathf.Max(0, layer);
    }
}
