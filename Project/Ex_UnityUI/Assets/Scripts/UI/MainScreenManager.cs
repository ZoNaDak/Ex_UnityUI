using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EX_UnityUI.Singleton;
using EX_UnityUI.MyInput;
using EX_UnityUI.MyEnum;
using EX_UnityUI.UI.BottomCanvas;

namespace EX_UnityUI.UI.MainCanvas {
    public enum ScreenType { SHOP, EQUIPMENT, STAGE, ABILITY, SETTING, END }
    public enum ScreenState {IDLE, MOVE, END}

    public class MainScreenManager : MonoSingleton<MainScreenManager> {
        const float SCREEN_MOVE_SPEED_MIN = 70f;
        const float SCREEN_MOVE_SPEED_LERP = 7.5f;
        const float TOUCH_LERP_MOVE_SPEED = 6f;
        const float REFERENCE_SPEED_FOR_SCREEN_MOVE = 10f;

        public MainScreenController[] MainScreenArray;

        private InputManager inputManager;
        private ScreenType currentType;
        private ScreenState state;
        private float screenSize_X;
        private float screenSpace_X;
        private Vector2 preLocalPosition;

        private Coroutine moveScreenCoroutine;

        public ScreenType CurrentType { get { return this.currentType; } }        
        public ScreenState State { get { return this.state; } }

        private MainScreenController CurrentScreen { get { return this.MainScreenArray[(int)this.CurrentType]; } }

        void Awake() {
            this.inputManager = InputManager.Instance;

            this.screenSize_X = this.MainScreenArray[0].RectTransform.sizeDelta.x;
            this.screenSpace_X = this.GetComponent<HorizontalLayoutGroup>().spacing;
            this.state = ScreenState.IDLE;
        }

        void Update() {
            if(this.state == ScreenState.IDLE && CheckTouchScreen()) {
                if(this.moveScreenCoroutine != null) {
                    StopCoroutine(this.moveScreenCoroutine);
                }
                StartCoroutine(DragScreen());
            }
        }

        public void MoveScreen(ScreenType _type) {
            this.currentType = _type;

            if(this.moveScreenCoroutine != null) {
                StopCoroutine(this.moveScreenCoroutine);
            }
            this.moveScreenCoroutine = StartCoroutine(MoveScreenAnimation(GetDestPos(_type)));
        }

        private void FollowTouch() {
            Vector2 touchPos =  this.inputManager.GetTouchPos();
            this.transform.Translate(new Vector2(
                Mathf.Lerp(this.CurrentScreen.transform.position.x, touchPos.x, TOUCH_LERP_MOVE_SPEED * Time.deltaTime) - this.CurrentScreen.transform.position.x,
                0f));
        }

        private void GoBackIdlePos() {
            this.moveScreenCoroutine = StartCoroutine(MoveScreenAnimation(GetDestPos(this.currentType)));
        }

        private bool CheckTouchScreen() {
            return this.MainScreenArray[(int)this.CurrentType].CheckTouchScreen();
        }

        private eDirection CheckScreenDragSpeed() {
            
            float screenSpeed = this.transform.localPosition.x - this.preLocalPosition.x;
            
            if(this.currentType != 0 && screenSpeed >= REFERENCE_SPEED_FOR_SCREEN_MOVE) {
                return eDirection.LEFT;
            } else if(this.currentType != ScreenType.END - 1 && screenSpeed <= -REFERENCE_SPEED_FOR_SCREEN_MOVE) {
                return eDirection.RIGHT;
            } else {
                return eDirection.NONE;
            }
        }

        private Vector2 GetDestPos(ScreenType _type) {
            Vector2 dest = new Vector2(-(screenSize_X + screenSpace_X) * (int)_type - screenSize_X * 0.5f, this.transform.localPosition.y);
            return dest;
        }

        //Coroutine#########################################################################
        private IEnumerator MoveScreenAnimation(Vector2 _dest) {
            this.state = ScreenState.MOVE;
            while(true) {
                if((_dest - (Vector2)this.transform.localPosition).magnitude <= Time.deltaTime * SCREEN_MOVE_SPEED_MIN) {
                    this.transform.localPosition = _dest;
                    break;
                } 
                this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, _dest, SCREEN_MOVE_SPEED_LERP * Time.deltaTime);
                yield return null;
            }

            this.state = ScreenState.IDLE;
        }

        private IEnumerator DragScreen() {
            this.state = ScreenState.MOVE;

            while(true){
                if(this.inputManager.TouchCount == 0) {
                    break;
                }
                this.preLocalPosition = this.transform.localPosition;
                FollowTouch();
                yield return null;
            }

            switch(CheckScreenDragSpeed()) {
                case eDirection.LEFT:
                    MoveScreen(this.currentType - 1);
                    break;
                case eDirection.RIGHT:
                    MoveScreen(this.currentType + 1);
                    break;
                default:
                    GoBackIdlePos();
                    break;    
            }
        }
        //###################################################################################
    }
}