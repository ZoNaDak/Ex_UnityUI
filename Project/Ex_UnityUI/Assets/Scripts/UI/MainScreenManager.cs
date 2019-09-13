using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EX_UnityUI.Singleton;

namespace EX_UnityUI.UI.MainCanvas {
    public enum ScreenType { SHOP, EQUIPMENT, STAGE, ABILITY, SETTING, END }
    public class MainScreenManager : MonoSingleton<MainScreenManager> {
        const float SCREEN_MOVE_SPEED_MIN = 70f;
        const float SCREEN_MOVE_SPEED_LERP = 7.5f;

        public MainScreenController[] MainScreenArray;

        private float screenSize_X;
        private float screenSpace_X;

        private Coroutine moveScreenAnimationCoroutine;

        void Awake() {
            this.screenSize_X = this.MainScreenArray[0].RectTransform.sizeDelta.x;
            this.screenSpace_X = this.GetComponent<HorizontalLayoutGroup>().spacing;
        }

        public void MoveScreen(ScreenType _type) {
            if(this.moveScreenAnimationCoroutine != null) {
                StopCoroutine(this.moveScreenAnimationCoroutine);
            }
            this.moveScreenAnimationCoroutine = StartCoroutine(MoveScreenAnimation(CalculateDestPos(_type)));
        }

        private Vector2 CalculateDestPos(ScreenType _type) {
            Vector2 dest = new Vector2(-(screenSize_X + screenSpace_X) * (int)_type - screenSize_X * 0.5f, this.transform.localPosition.y);
            return dest;
        }

        //Coroutine#########################################################################
        private IEnumerator MoveScreenAnimation(Vector2 _dest) {
            while(true) {
                if((_dest - (Vector2)this.transform.localPosition).magnitude <= Time.deltaTime * SCREEN_MOVE_SPEED_MIN) {
                    this.transform.localPosition = _dest;
                    break;
                } 
                this.transform.localPosition = Vector2.Lerp(this.transform.localPosition, _dest, SCREEN_MOVE_SPEED_LERP * Time.deltaTime);
                yield return null;
            }
        }
        //###################################################################################
    }
}