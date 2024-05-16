using System.Collections;
using System.Collections.Generic;
using SensenToolkit;
using TMPro;
using UnityEngine;

namespace Sensen.Components
{
    public class TextAlpha : IAlphaCompatible
    {
        private TMP_Text text;

        public float Alpha { get => text.color.a; set => text.color = text.color.WithAlpha(value); }
        public Transform Transform => text.transform;

        public TextAlpha(TMP_Text text)
        {
            this.text = text;
        }
    }
}
