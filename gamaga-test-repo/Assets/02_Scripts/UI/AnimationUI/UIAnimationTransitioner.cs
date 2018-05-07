using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class UIAnimationTransitioner : UITransitioner
{
    [SerializeField]
    private AnimationClip m_openAnimation;

    [SerializeField]
    private AnimationClip m_closeAnimation;

    private Animation m_animation;
    private AnimationPlayer m_player;

    private void Awake()
    {
        m_animation = GetComponent<Animation>();
        m_player = new AnimationPlayer(m_animation);
    }

    public override void PlayCloseAnimation(Action<bool> onAnimationFinished)
    {
        m_player.CleanCallbacks();
        m_player.Play(m_closeAnimation, PlayMode.StopSameLayer, onAnimationFinished);
    }

    public override void PlayOpenAnimation(Action<bool> onAnimationFinished)
    {
        m_player.CleanCallbacks();
        m_player.Play(m_openAnimation, PlayMode.StopSameLayer, onAnimationFinished);
    }

    private void Update()
    {
        m_player.UpdateAnimationPlayer(TimeManager.Instance.DeltaTime);
    }

    private class AnimationPlayer
    {
        private Animation m_animations;

        private float m_clipDuration = 0f;
        private float m_elapsedTime = 0f;
        private bool m_playingAnimation = false;

        private Action<bool> m_onAnimationFinished;

        public AnimationPlayer(Animation animations)
        {
            m_animations = animations;
        }

        public void Play(AnimationClip anim, PlayMode mode = PlayMode.StopSameLayer, Action<bool> onAnimationFinished = null)
        {
            if (m_animations.GetClip(anim.name) == null)
            {
                m_animations.AddClip(anim, anim.name);
            }
            Play(anim.name, mode, onAnimationFinished);
        }

        public void Play(string animName, PlayMode mode = PlayMode.StopSameLayer, Action<bool> onAnimationFinished = null)
        {
            m_onAnimationFinished = onAnimationFinished;
            m_animations.Play(animName, mode);
            m_elapsedTime = 0;
            m_clipDuration = m_animations.GetClip(animName).averageDuration;
            m_playingAnimation = true;
        }

        public void StopAnimation(bool animationStopped = true)
        {
            m_playingAnimation = false;
            m_animations.Stop();
            if(m_onAnimationFinished != null)
            {
                m_onAnimationFinished(animationStopped);
            }            
            m_onAnimationFinished = null;
        }

        public void UpdateAnimationPlayer(float deltaTime)
        {
            if (m_playingAnimation)
            {
                m_elapsedTime += deltaTime;
                if (m_elapsedTime >= m_clipDuration)
                {
                    StopAnimation(false);
                }
            }
        }

        public void CleanCallbacks()
        {
            m_onAnimationFinished = null;
        }
    }
}
