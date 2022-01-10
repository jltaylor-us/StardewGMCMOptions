// Copyright 2022 Jamie Taylor
﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;

namespace GMCMOptions.Framework {
    /// <summary>
    /// Implementation of the <c cref="IGMCMOptionsAPI">IGMCMOptionsAPI</c>.
    /// </summary>
    public class API : IGMCMOptionsAPI {
        private readonly IModRegistry modRegistry;
        public API(IModRegistry modRegistry) {
            this.modRegistry = modRegistry;
        }

        /// <inheritdoc/>
        public void AddColorOption(IManifest mod, Func<Color> getValue, Action<Color> setValue, Func<string> name,
            Func<string> tooltip = null, bool showAlpha = true,
            uint colorPickerStyle = 0, string fieldId = null) {
            var gmcm = modRegistry.GetApi<GMCMAPI>("spacechase0.GenericModConfigMenu");
            if (gmcm == null) return;
            ColorPickerOption option = new ColorPickerOption(getValue, setValue, showAlpha, (ColorPickerStyle)colorPickerStyle);
            gmcm.AddComplexOption(
                mod: mod,
                name: name,
                tooltip: tooltip,
                draw: option.Draw,
                height: option.Height,
                beforeMenuOpened: option.Reset,
                beforeSave: option.SaveChanges,
                afterReset: option.Reset,
                fieldId: fieldId);
        }
    }
    /// <summary>
    /// The portion of the GMCM API that we need
    /// </summary>
    public interface GMCMAPI {
        // see https://github.com/spacechase0/StardewValleyMods/blob/develop/GenericModConfigMenu/IGenericModConfigMenuApi.cs
        void AddComplexOption(IManifest mod, Func<string> name, Action<SpriteBatch, Vector2> draw, Func<string> tooltip = null, Action beforeMenuOpened = null, Action beforeSave = null, Action afterSave = null, Action beforeReset = null, Action afterReset = null, Action beforeMenuClosed = null, Func<int> height = null, string fieldId = null);

    }

}
