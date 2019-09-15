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
                    /*
                    Touch touch = Input.GetTouch(i);
                    if(this.RectTransform.rect.Contains(touch.deltaPosition)) {
                        Debug.Log("Hi");
                        return true;
                    }*/
                    Debug.Log(i);
                }
                this.state = ScreenState.MOVE;
            }

            return false;
        }

        //Coroutine######
        private IEnumerator DragScreen() {
            this.state = ScreenState.MOVE;

            while(true){
                yield return null;
            }
        }
        //###############
    }
}