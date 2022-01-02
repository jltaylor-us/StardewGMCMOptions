// Copyright 2022 Jamie Taylor
//
// To facilitate other mods which would like to use the GMCMOptions API,
// the license for this file (and only this file) is modified by removing the
// notice requirements for binary distribution.  The license (as amended)
// is included below, making this file self-contained.
//
// In other words, anyone may copy this file into their own mod.
//

//  Copyright(c) 2022, Jamie Taylor
//All rights reserved.
//
//Redistribution and use in source and binary forms, with or without
//modification, are permitted provided that the following conditions are met:
//
//1.Redistributions of source code must retain the above copyright notice, this
//   list of conditions and the following disclaimer.
//
//2. [condition removed for this file]
//
//3. Neither the name of the copyright holder nor the names of its
//   contributors may be used to endorse or promote products derived from
//   this software without specific prior written permission.
//
//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
//AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
//IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
//FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
//DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
//SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
//CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
//OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
//OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;

namespace GMCMOptions {
    /// <summary>The API which lets other mods add a config UI using one of the complex options defined in GMCMOptions.</summary>
    public interface IGMCMOptionsAPI {
        /// <summary>Add a <c cref="Color">Color</c> option at the current position in the GMCM form.</summary>
        /// <param name="mod">The mod's manifest.</param>
        /// <param name="getValue">Get the current value from the mod config.</param>
        /// <param name="setValue">Set a new value in the mod config.</param>
        /// <param name="name">The label text to show in the form.</param>
        /// <param name="tooltip">The tooltip text shown when the cursor hovers on the field, or <c>null</c> to disable the tooltip.</param>
        /// <param name="showAlpha">Whether the color picker should allow setting the Alpha channel</param>
        /// <param name="colorPickerStyle">Flags to control how the color picker is rendered.  <see cref="ColorPickerStyle"/></param>
        /// <param name="fieldId">The unique field ID for use with GMCM's <c>OnFieldChanged</c>, or <c>null</c> to auto-generate a randomized ID.</param>
        void AddColorOption(IManifest mod, Func<Color> getValue, Action<Color> setValue, Func<string> name,
            Func<string> tooltip = null, bool showAlpha = true, uint colorPickerStyle = 0, string fieldId = null);

        #pragma warning disable format
        /// <summary>
        /// Flags to control how the <c cref="ColorPickerOption">ColorPickerOption</c> widget is displayed.
        /// </summary>
        [Flags]
        public enum ColorPickerStyle : uint {
            Default = 0,
            RGBSliders    = 0b00000001,
            HSVColorWheel = 0b00000010,
            HSLColorWheel = 0b00000100,
            AllStyles     = 0b11111111,
            NoChooser     = 0,
            RadioChooser  = 0b01 << 8,
            ToggleChooser = 0b10 << 8
        }
        #pragma warning restore format
    }

}
