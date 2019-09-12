using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EX_UnityUI.UI.BottomCanvas {
    public enum ButtonType { ONE, TWO, THREE, FOUR, FIVE, END }
    public class BottomUIButtonCotroller : MonoBehaviour {
        public Button button;

        private RectTransform rectTransform;
        private ButtonType type;

        void Awake() {
            this.rectTransform = this.transform as RectTransform;
        }

        void Start() {
            this.rectTransform.localScale = new Vector2(1, 1);
        }

        public void SetType(ButtonType _type){
            this.type = _type;
        }

        public void Click() {
            this.button.interactable = false;
            BottomCanvasController.Instance.DeclickButtons(this);
        }

        public void Declick() {
            this.button.interactable = true;
        }
    }
}