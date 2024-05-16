using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sensen.Components
{
    public interface IAlphaCompatible
    {
        float Alpha { get; set; }
        Transform Transform { get; }
    }
}
