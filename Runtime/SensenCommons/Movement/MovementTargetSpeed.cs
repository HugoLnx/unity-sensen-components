using LnxArch;
using UnityEngine;

namespace SensenToolkit.Commons
{
    public class MovementTargetSpeed : LnxComponent<(float initial, float final, float target)>
    {
        public float InitialValue
        {
            get => Value.initial;
            set {
                var (_, final, target) = Value;
                Value = (value, final, target);
            }
        }
        public float FinalValue
        {
            get => Value.final;
            set {
                var (initial, _, target) = Value;
                Value = (initial, value, target);
            }
        }
        public float TargetValue
        {
            get => Value.target;
            set {
                var (initial, final, _) = Value;
                Value = (initial, final, value);
            }
        }
    }
}
