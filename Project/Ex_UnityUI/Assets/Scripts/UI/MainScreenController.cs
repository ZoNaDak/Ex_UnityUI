using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EX_UnityUI.MyInput;

namespace EX_UnityUI.UI.MainCanvas {
    public enum ScreenState { OFF, IDLE, MOVE, END}
    public class MainScreenController : MonoBehaviour {
        public RectTransform RectTransform;

        private ScreenState state;

        void Awake() {
            this.state = ScreenState.OFF;
        }

        void Update() {
            if(this.state == ScreenState.IDLE && CheckTouchScreen()) {
                StartCoroutine(DragScreen());
            }
        }

        public void SetActivate(bool _isActivate) {
            if(_isActivate) {
                this.state = ScreenState.IDLE;
            } else {
                this.state = ScreenState.OFF;
            }
        }

        private bool CheckTouchScreen() {
            if(InputManager.Instance.TouchCount > 0) {
                for(int i = 0; i < InputManager.Instance.TouchCount; ++i) {
                    if(InputManager.Instance.CheckTouchArea(this.RectTransform)) {
                        return true;
                    }
                }
            }

            return false;
        }

        //Coroutine######
        private IEnumerator DragScreen() {
            this.state = ScreenState.MOVE;
            Debug.LogWarning("Hi");

            while(true){
                yield return null;
            }
        }
        //###############
    }
}