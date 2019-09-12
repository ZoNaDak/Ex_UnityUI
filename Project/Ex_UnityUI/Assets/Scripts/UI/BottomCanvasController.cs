using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EX_UnityUI.UI.BottomCanvas {
    public class BottomCanvasController : MonoBehaviour {
        public BottomUIButtonCotroller[] ButtonArray;

        void Start() {
            for(int i = 0; i < this.ButtonArray.Length; ++i) {
                this.ButtonArray[i].SetType((ButtonType)i);
            }
        }
    }
}