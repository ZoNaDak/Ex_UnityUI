using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EX_UnityUI.Singleton;

namespace EX_UnityUI.MyInput {
    public class InputManager : Singleton<InputManager> {
        public int TouchCount {
            get {
                int count = 0;

                #if UNITY_EDITOR
                    if(Input.GetMouseButton(0)) {
                        count = 1;
                    }
                #elif UNITY_ANDROID
                    count = Input.touchCount;
                #endif

                return count;
            }
        }

        public bool CheckTouchArea(RectTransform _rectTransform) {
            Vector2 touchPos = GetTouchPos(this.TouchCount - 1);
            _rectTransform.rect.Contains(touchPos);
            return RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, touchPos);
        }

        public Vector2 GetTouchPos() {
            return GetTouchPos(this.TouchCount - 1);
        }

        public Vector2 GetTouchPos(int _idx) {
            Vector2 pos = new Vector2();
            
            #if UNITY_EDITOR
                if(_idx != 0) {
                    throw new System.ArgumentException("Can't Get Touch Pos : Invalid Index.");
                }
                pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            #elif UNITY_ANDROID
                pos = Input.GetTouch(_idx).deltaPosition;
            #endif

            return pos;
        }
    }
}