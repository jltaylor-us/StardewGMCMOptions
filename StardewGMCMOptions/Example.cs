// Copyright 2022 Jamie Taylor
﻿using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;

namespace GMCMOptions {
    /// <summary>
    /// An example showing usage of the complex options available in the GMCMOptions API.
    /// </summary>
    public class Example {
        /// <summary>
        /// An example configuration object.
        /// </summary>
        public class Config {
            public Color c1 = Color.BlueViolet;
            public Color c2 = Color.MediumAquamarine;
            public Color c3 = Color.SandyBrown;
            public Color c4 = Color.ForestGreen;
        }

        /// <summary>
        /// The current configuration value.
        /// </summary>
        private Config config;

        private IManifest ModManifest;
        private IModHelper Helper;
        public Example(IManifest manifest, IModHelper helper) {
            ModManifest = manifest;
            Helper = helper;
            // normally this would read the existing config: config = helper.ReadConfig<Config>();
            // but we don't actually have (or want) a config.json file
            config = new Config();
        }

        private void SaveConfig() {
            // normally this would save the config to a file: Helper.WriteConfig(config);
            // but we don't actually have (or want) a config.json file.
            // do nothing.
        }

        public void AddToGMCM() {
            var configMenu = Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            var configMenuExt = Helper.ModRegistry.GetApi<IGMCMOptionsAPI>("jltaylor-us.GMCMOptions");
            if (configMenu is null || configMenuExt is null) {
                return;
            }
            // register the mod
            configMenu.Register(
                mod: ModManifest,
                reset: () => config = new Config(),
                save: SaveConfig);
            // register some complex config options
            configMenuExt.AddColorOption(
                mod: ModManifest,
                getValue: () => config.c4,
                setValue: (c) => config.c4 = c,
                name: () => "Default options",
                tooltip: () => "This example shows the AddColorOption default options");
            configMenuExt.AddColorOption(
                mod: ModManifest,
                getValue: () => config.c1,
                setValue: (c) => config.c1 = c,
                name: () => "Simple RGBA sliders",
                tooltip: () => "This example shows a single style of color picker (the RGB sliders), with alpha.",
                colorPickerStyle: (uint)IGMCMOptionsAPI.ColorPickerStyle.RGBSliders);
            configMenuExt.AddColorOption(
                mod: ModManifest,
                getValue: () => config.c2,
                setValue: (c) => config.c2 = c,
                name: () => "Single Picker, no alpha",
                tooltip: () => "This example shows all different picker styles, but only one at a time, with no alpha slider.",
                showAlpha: false,
                colorPickerStyle: (uint)(IGMCMOptionsAPI.ColorPickerStyle.AllStyles | IGMCMOptionsAPI.ColorPickerStyle.RadioChooser));
            configMenuExt.AddColorOption(
                mod: ModManifest,
                getValue: () => config.c3,
                setValue: (c) => config.c3 = c,
                name: () => "All Pickers, with no alpha",
                tooltip: () => "This example shows all different picker styles, with multiple visible at a time, with no alpha slider.",
                showAlpha: false,
                colorPickerStyle: (uint)(IGMCMOptionsAPI.ColorPickerStyle.AllStyles | IGMCMOptionsAPI.ColorPickerStyle.ToggleChooser));
        }

        public void RemoveFromGMCM() {
            var configMenu = Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            configMenu.Unregister(ModManifest);
        }
    }
    public interface IGenericModConfigMenuApi {
        void Register(IManifest mod, Action reset, Action save, bool titleScreenOnly = false);
        void Unregister(IManifest mod);
    }
}
