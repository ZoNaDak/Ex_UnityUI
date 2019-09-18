using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EX_UnityUI.MyInput;

namespace EX_UnityUI.UI.MainCanvas {
    public class MainScreenController : MonoBehaviour {
        

        public RectTransform RectTransform;

        private InputManager inputManager;

        void Awake() {
            this.inputManager = InputManager.Instance;
        }

        internal bool CheckTouchScreen() {
            if(this.inputManager.TouchCount > 0) {
                for(int i = 0; i < this.inputManager.TouchCount; ++i) {
                    if(this.inputManager.CheckTouchArea(this.RectTransform)) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}