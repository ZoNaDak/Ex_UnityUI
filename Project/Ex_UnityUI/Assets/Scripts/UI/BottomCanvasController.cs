﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EX_UnityUI.Singleton;

namespace EX_UnityUI.UI.BottomCanvas {
    public class BottomCanvasController : MonoSingleton<BottomCanvasController> {
        const float BUTTONCOVER_MOVE_SPEED = 5f;
        public BottomUIButtonCotroller[] ButtonArray;
        public RectTransform ButtonCover;

        private RectTransform rectTransform;

        private Coroutine buttonCoverMoveCoroutine;

        void Awake() {
            this.rectTransform = this.transform as RectTransform;
        }

        void Start() {
            for(int i = 0; i < this.ButtonArray.Length; ++i) {
                if(i == (int)ButtonType.THREE) {
                    this.ButtonArray[i].SetType((ButtonType)i, true);
                } else {
                    this.ButtonArray[i].SetType((ButtonType)i, false);
                }
            }
        }

        public void DeclickButtons(BottomUIButtonCotroller _caller) {
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

        //Coroutine#########################################################################
        private IEnumerator RebuildLayout(BottomUIButtonCotroller _caller) {
            while(true) {
                LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform);
                if(_caller.transform.localScale.x == BottomUIButtonCotroller.CLICKED_SCALE_X) {
                    break;
                }
                yield return null;
            }
        }

        private IEnumerator MoveButtonCover(BottomUIButtonCotroller _caller) {
            while(true) {
                Vector2 moveDir = _caller.RectTransform.position - this.ButtonCover.position;
                if(moveDir.magnitude <= BUTTONCOVER_MOVE_SPEED * Time.deltaTime) {
                    this.ButtonCover.position = _caller.RectTransform.position;
                    break;
                }
                this.ButtonCover.position += (_caller.RectTransform.position - this.ButtonCover.position) * BUTTONCOVER_MOVE_SPEED * Time.deltaTime;
                yield return null;
            }
            this.buttonCoverMoveCoroutine = null;
        }
        //###################################################################################
    }
}