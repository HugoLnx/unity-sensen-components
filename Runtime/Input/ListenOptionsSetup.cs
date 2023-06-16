using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using System;

namespace Sensen.Components
{
    public static class ListenOptionsSetup
    {

        public static BindingListenOptions CreateDefaultOptionsForKeyboard()
        {
            BindingListenOptions opts = CreateDefaultOptions();
            ToKeyboardOnly(opts);
            return opts;
        }

        public static BindingListenOptions CreateDefaultOptionsForController()
        {
            BindingListenOptions opts = CreateDefaultOptions();
            ToControllerOnly(opts);
            return opts;
        }

        public static BindingListenOptions CreateDefaultOptions()
        {
            return new BindingListenOptions
            {
                IncludeUnknownControllers = true,
                IncludeNonStandardControls = true,
                IncludeMouseButtons = true,
                IncludeKeys = true,
                IncludeModifiersAsFirstClassKeys = true,
                MaxAllowedBindings = 0,
                MaxAllowedBindingsPerType = 0,
                AllowDuplicateBindingsPerSet = false,
                UnsetDuplicateBindingsOnSet = true,
                ReplaceBinding = null,
                OnBindingFound = null,
                OnBindingAdded = null,
                OnBindingRejected = null
            };
        }
        public static void ToKeyboardOnly(BindingListenOptions opts)
        {
            opts.IncludeKeys = true;
            opts.IncludeMouseButtons = true;
            opts.IncludeMouseScrollWheel = true;
            opts.IncludeControllers = false;
            opts.IncludeNonStandardControls = false;
            opts.IncludeUnknownControllers = false;
        }

        public static void ToControllerOnly(BindingListenOptions opts)
        {
            opts.IncludeKeys = false;
            opts.IncludeMouseButtons = false;
            opts.IncludeMouseScrollWheel = false;
            opts.IncludeControllers = true;
            opts.IncludeNonStandardControls = true;
            opts.IncludeUnknownControllers = true;
        }
    }
}
