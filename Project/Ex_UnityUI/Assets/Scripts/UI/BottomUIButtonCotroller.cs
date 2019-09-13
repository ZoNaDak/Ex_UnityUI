using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EX_UnityUI.UI.BottomCanvas {
    public enum ButtonType { ONE, TWO, THREE, FOUR, FIVE, END }
    public class BottomUIButtonCotroller : MonoBehaviour {
        public const float CLICKED_SCALE_X = 1.7f;
        public Button Button;
        public BottomUIButtonFrontController ButtonFront;

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

        public void UpdateButtonFrontPos() {
            this.ButtonFront.transform.position = new Vector3(
                this.transform.position.x, 
                this.ButtonFront.transform.position.y,
                this.ButtonFront.transform.position.z);
        }

        //Button Click Function#########################################################################
        public void Click() {
            this.Button.interactable = false;
            this.ButtonFront.Activate(this.rectTransform);
            BottomCanvasController.Instance.DeclickButtons(this);
        }

        public void Declick() {
            this.Button.interactable = true;
            this.ButtonFront.Deactivate(this.rectTransform);
        }
        //##############################################################################################
    }
}