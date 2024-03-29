// Copyright 2022 Jamie Taylor
﻿using System;
using GMCMOptions.Framework;
using HarmonyLib;
using StardewModdingAPI;

namespace GMCMOptions {
    public class ModEntry : Mod {
        private Example? example;
        public ModEntry() {
        }

        public override void Entry(IModHelper helper) {
            TooltipHelper.Init(new Harmony(this.ModManifest.UniqueID), Monitor);
            helper.ConsoleCommands.Add("gmcmoptions-example", "Control the GMCMOptions example config menu.\n\nUsage: gmcmoptions-example [help|enable|disable]", this.ExampleCLI);
        }

        public override object GetApi(IModInfo mod) {
            return new API(Helper, Monitor, mod);
        }

        private void ExampleCLI(string command, string[] args) {
            if (args.Length > 1) {
                Monitor.Log("Too many arguments\nUsage: gmcmoptions-example [enable|disable]", LogLevel.Error);
                return;
            }
            bool enable = true;
            if (args.Length == 1) {
                if (args[0] == "enable") {
                    enable = true;
                } else if (args[0] == "disable") {
                    enable = false;
                } else if (args[0] == "help") {
                    Monitor.Log("Usage: gmcmoptions-example [help|enable|disable]", LogLevel.Info);
                    Monitor.Log($"Adds or removes the example Generic Mod Config Menu for {ModManifest.Name}", LogLevel.Info);
                    Monitor.Log($"The menu is currently {(example is null ? "disabled" : "enabled")}", LogLevel.Info);
                    return;
                } else {
                    Monitor.Log($"Unknown arg.  Expected \"enable\" or \"disable\", but got \"{args[0]}\"", LogLevel.Error);
                    return;
                }
            }
            if (enable) {
                if (example is not null) {
                    Monitor.Log("Example already enabled; ignoring", LogLevel.Info);
                    return;
                }
                example = new Example(ModManifest, Helper);
                example.AddToGMCM();
                Monitor.Log("Example menu enabled", LogLevel.Info);
            } else {
                example?.RemoveFromGMCM();
                example = null;
                Monitor.Log("Example menu disabled", LogLevel.Info);
            }
        }
    }
}
