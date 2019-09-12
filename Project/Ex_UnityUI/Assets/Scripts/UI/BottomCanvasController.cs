using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EX_UnityUI.Singleton;

namespace EX_UnityUI.UI.BottomCanvas {
    public class BottomCanvasController : MonoSingleton<BottomCanvasController> {
        public BottomUIButtonCotroller[] ButtonArray;

        private RectTransform rectTransform;

        void Awake() {
            this.rectTransform = this.transform as RectTransform;
        }

        void Start() {
            for(int i = 0; i < this.ButtonArray.Length; ++i) {
                if(i == (int)ButtonType.THREE) {
                    this.ButtonArray[i].SetType((ButtonType)i, true);
                } else {
                    this.ButtonArray[i].SetType((ButtonType)i, false);
                }
            }
        }

        public void DeclickButtons(BottomUIButtonCotroller _caller) {
            for(int i = 0; i < this.ButtonArray.Length; ++i) {
                if(this.ButtonArray[i] != _caller) {
                    this.ButtonArray[i].Declick();
                }
            }
            StartCoroutine(RebuildLayout(_caller));
        }

        //Coroutine
        private IEnumerator RebuildLayout(BottomUIButtonCotroller _caller) {
            while(true) {
                LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform);
                if(_caller.transform.localScale.x == BottomUIButtonCotroller.CLICKED_SCALE_X) {
                    break;
                }
                yield return null;
            }
        }
    }
}