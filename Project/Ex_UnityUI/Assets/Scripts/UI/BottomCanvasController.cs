using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EX_UnityUI.Singleton;

namespace EX_UnityUI.UI.BottomCanvas {
    public class BottomCanvasController : MonoSingleton<BottomCanvasController> {
        public BottomUIButtonCotroller[] ButtonArray;

        void Start() {
            for(int i = 0; i < this.ButtonArray.Length; ++i) {
                this.ButtonArray[i].SetType((ButtonType)i);
            }
        }

        public void DeclickButtons(BottomUIButtonCotroller _caller) {
            for(int i = 0; i < this.ButtonArray.Length; ++i) {
                if(this.ButtonArray[i] != _caller) {
                    this.ButtonArray[i].Declick();
                }
            }
        }
    }
}