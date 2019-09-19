using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EX_UnityUI.Singleton;
using EX_UnityUI.UI.MainCanvas;

namespace EX_UnityUI.UI.BottomCanvas {
    public class BottomCanvasController : MonoSingleton<BottomCanvasController> {
        const float BUTTONCOVER_MOVE_SPEED = 6f;
        public BottomUIButtonCotroller[] ButtonArray;
        public RectTransform ButtonCover;
        public RectTransform ButtonParent;

        private Coroutine buttonCoverMoveCoroutine;
        private ScreenType currentButtonType;

        private BottomUIButtonCotroller CurrentButton { get { return this.ButtonArray[(int)this.currentButtonType]; }}

        void Start() {
            this.currentButtonType = ScreenType.STAGE;
            for(int i = 0; i < this.ButtonArray.Length; ++i) {
                if(i == (int)this.currentButtonType) {
                    this.ButtonArray[i].SetType((ScreenType)i, true);
                } else {
                    this.ButtonArray[i].SetType((ScreenType)i, false);
                }
            }
        }

        public void SetCurrentButton(BottomUIButtonCotroller _caller) {
            this.currentButtonType = _caller.Type;
            for(int i = 0; i < this.ButtonArray.Length; ++i) {
                if(this.ButtonArray[i] != _caller) {
                    this.ButtonArray[i].Declick();
                }
            }
            StartCoroutine(RebuildLayout(_caller));
            if(this.buttonCoverMoveCoroutine != null) {
                StopCoroutine(this.buttonCoverMoveCoroutine);
            }
            this.buttonCoverMoveCoroutine = StartCoroutine(MoveButtonCover(_caller));
        }

        public void SetCurrentButton(ScreenType _screenType) {
            this.ButtonArray[(int)_screenType].Activate();
            SetCurrentButton(this.ButtonArray[(int)_screenType]);
        }

        public void FollowButtonCoverToMainScreen(float _moveRate) {
            if(!CheckIsMoveableButtonCover(_moveRate)) {
                return;
            }
            this.ButtonCover.localPosition = new Vector2(
                this.CurrentButton.transform.localPosition.x + this.ButtonCover.sizeDelta.x * _moveRate,
                this.ButtonCover.localPosition.y);
        }

        private bool CheckIsMoveableButtonCover(float _moveRate) {
            if((this.currentButtonType == ScreenType.END - 1 && _moveRate >= 0) 
                || this.currentButtonType == 0 && _moveRate <= 0) {
                return false;
            }
            return true;
        }

        //Coroutine#########################################################################
        private IEnumerator RebuildLayout(BottomUIButtonCotroller _caller) {
            while(true) {
                LayoutRebuilder.ForceRebuildLayoutImmediate(this.ButtonParent);
                for(int i = 0; i < this.ButtonArray.Length; ++i) {
                    this.ButtonArray[i].UpdateButtonFrontPos();
                }
                if(_caller.transform.localScale.x == BottomUIButtonCotroller.CLICKED_SCALE_X) {
                    break;
                }
                yield return null;
            }
        }

        private IEnumerator MoveButtonCover(BottomUIButtonCotroller _caller) {
            while(true) {
                Vector2 moveDir = _caller.RectTransform.localPosition - this.ButtonCover.localPosition;
                if(_caller.transform.localScale.x == BottomUIButtonCotroller.CLICKED_SCALE_X 
                    && moveDir.magnitude <= BUTTONCOVER_MOVE_SPEED * Time.deltaTime) {
                    this.ButtonCover.localPosition = _caller.RectTransform.localPosition;
                    break;
                }
                this.ButtonCover.localPosition += (Vector3)moveDir * BUTTONCOVER_MOVE_SPEED * Time.deltaTime;
                yield return null;
            }
            this.buttonCoverMoveCoroutine = null;
        }
        //###################################################################################
    }
}