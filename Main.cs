using HarmonyLib;
using Il2CppAssets.Scripts.Models.Rounds;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using MelonLoader;
using System;
using UnityEngine;
using Random = System.Random;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;
using Il2CppAssets.Scripts.Utils;
using UnityEngine.PlayerLoop;

namespace BossBloonsMod {
    public class Main : MelonMod {
        static string[] bosses = { "Bloonarius", "Lych", "Vortex", "Dreadbloon" };
        private int bossLevel = 1;

        [System.Obsolete]
        public override void OnApplicationStart() {
            base.OnApplicationStart();
            //Logger.Log("Voice Activation Has Loaded");
        }

        public override void OnUpdate() {
            base.OnUpdate();

            if (InGame.instance != null) {
                if (InGame.instance.bridge != null) {
                    if (Input.GetKeyDown(KeyCode.F1)) {
                        InGame.instance.bridge.SpawnBloons(GetBloomEmissionArray(bosses[0] + bossLevel, 1, 0), ((bossLevel - 1) * 20) + 40, 0);
                    } else if (Input.GetKeyDown(KeyCode.F2)) {
                        InGame.instance.bridge.SpawnBloons(GetBloomEmissionArray(bosses[1] + bossLevel, 1, 0), ((bossLevel - 1) * 20) + 40, 0);
                    } else if (Input.GetKeyDown(KeyCode.F3)) {
                        InGame.instance.bridge.SpawnBloons(GetBloomEmissionArray(bosses[2] + bossLevel, 1, 0), ((bossLevel - 1) * 20) + 40, 0);
                    } else if (Input.GetKeyDown(KeyCode.F4)) {
                        InGame.instance.bridge.SpawnBloons(GetBloomEmissionArray(bosses[3] + bossLevel, 1, 0), ((bossLevel - 1) * 20) + 40, 0);
                    } else if (Input.GetKeyDown(KeyCode.F5)) {
                        PopupScreen.instance.ShowSetValuePopup("Boss Level", "Sets the boss level between 1 and 5",
                            new Action<int>(i => {
                                if (i < 1) { i = 1; }
                                if (i > 5) { i = 5; }
                                bossLevel = i;
                            }), 1);
                    }
                }
            }
        }

        public Il2CppReferenceArray<BloonEmissionModel> GetBloomEmissionArray(String bloon, int amount, int spawnTime) {
            Il2CppReferenceArray<BloonEmissionModel> bme = new Il2CppReferenceArray<BloonEmissionModel>(amount);
            for (int i = 0; i < bme.Length; i++) {
                bme[i] = new BloonEmissionModel(bloon, i * spawnTime, bloon);
            }
            return bme;
        }
    }
}
