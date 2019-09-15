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
    }
}