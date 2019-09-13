using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EX_UnityUI.Singleton;

namespace EX_UnityUI.UI.MainCanvas {
    public enum ScreenType { SHOP, EQUIPMENT, STAGE, ABILITY, SETTING, END }
    public class MainScreenManager : MonoSingleton<MainScreenManager> {
        public MainScreenController[] MainScreenArray;

        private float screenSize_X;
        private float screenSpace_X;

        void Awake() {
            this.screenSize_X = this.MainScreenArray[0].RectTransform.sizeDelta.x;
            this.screenSpace_X = this.GetComponent<HorizontalLayoutGroup>().spacing;
        }

        public void MoveScreen(ScreenType _type) {
            this.transform.localPosition = CalculateDestPos(_type);
        }

        private Vector2 CalculateDestPos(ScreenType _type) {
            Vector2 dest = new Vector2(-(screenSize_X + screenSpace_X) * (int)_type - screenSize_X * 0.5f, this.transform.localPosition.y);
            return dest;
        }
    }
}