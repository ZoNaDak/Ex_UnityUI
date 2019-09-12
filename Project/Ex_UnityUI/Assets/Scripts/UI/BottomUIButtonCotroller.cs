using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EX_UnityUI.UI.BottomCanvas {
    public enum ButtonType { ONE, TWO, THREE, FOUR, FIVE, END }
    public class BottomUIButtonCotroller : MonoBehaviour {
        public const float CLICKED_SCALE_X = 1.7f;
        public Button button;

        private RectTransform rectTransform;
        private ButtonType type;

        public RectTransform RectTransform { get { return this.rectTransform; } }

        void Awake() {
            this.rectTransform = this.transform as RectTransform;
        }

        void Start() {
            this.rectTransform.localScale = new Vector2(1, 1);
        }

        public void SetType(ButtonType _type, bool _isActivated){
            this.type = _type;
            if(_isActivated) {
                Click();
            }
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