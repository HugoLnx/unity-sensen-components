using System.Collections;
using System.Collections.Generic;
using SensenToolkit;
using UnityEngine;

namespace Sensen.Components
{
    public class SpriteAlpha : IAlphaCompatible
    {
        private SpriteRenderer sprite;

        public float Alpha { get => sprite.color.a; set => sprite.color = sprite.color.WithAlpha(value); }
        public Transform Transform => sprite.transform;

        public SpriteAlpha(SpriteRenderer sprite)
        {
            this.sprite = sprite;
        }
    }
}
