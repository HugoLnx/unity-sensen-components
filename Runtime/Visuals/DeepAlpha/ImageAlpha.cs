using System.Collections;
using System.Collections.Generic;
using SensenToolkit;
using UnityEngine;
using UnityEngine.UI;

namespace Sensen.Components
{
    public class ImageAlpha : IAlphaCompatible
    {
        private readonly Image img;

        public float Alpha { get => img.color.a; set => img.color = img.color.WithAlpha(value); }
        public Transform Transform => img.transform;

        public ImageAlpha(Image img)
        {
            this.img = img;
        }
    }
}
