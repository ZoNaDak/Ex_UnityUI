using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EX_UnityUI.UI.BottomCanvas {
    public class BottomUIButtonFrontController : MonoBehaviour {
        const float IMAGE_UP_POS_Y = 30f;
        const float IMAGE_MOVE_SPEED = 250f;

        public RectTransform ImageTransform;
        public TextMeshProUGUI Text;

        private Coroutine moveCoroutine;

        public void Activate(RectTransform _caller) {
            if(this.moveCoroutine != null) {
                StopCoroutine(this.moveCoroutine);
            }
            this.moveCoroutine = StartCoroutine(MoveUpImage());
        }

        public void Deactivate(RectTransform _caller) {
            if(this.moveCoroutine != null) {
                StopCoroutine(this.moveCoroutine);
            }
            this.moveCoroutine = StartCoroutine(MoveDownImage());
        }

        //Coroutine#########################################################################
        private IEnumerator MoveUpImage() {
            while(true) {
                if(IMAGE_UP_POS_Y - this.ImageTransform.transform.localPosition.y <= IMAGE_MOVE_SPEED * Time.deltaTime) {
                    this.ImageTransform.transform.localPosition = new Vector2(this.ImageTransform.transform.localPosition.x, IMAGE_UP_POS_Y);
                    break;
                }
                this.ImageTransform.transform.Translate(0, IMAGE_MOVE_SPEED * Time.deltaTime, 0); 
                yield return null;
            }
            this.Text.gameObject.SetActive(true);
            this.moveCoroutine = null;
        }

        private IEnumerator MoveDownImage() {
            while(true) {
                if(this.ImageTransform.transform.localPosition.y <= IMAGE_MOVE_SPEED * Time.deltaTime) {
                    this.ImageTransform.transform.localPosition = new Vector2(this.ImageTransform.transform.localPosition.x, 0f);
                    break;
                }
                this.ImageTransform.transform.Translate(0, -IMAGE_MOVE_SPEED * Time.deltaTime, 0);
                yield return null;
            }
            this.Text.gameObject.SetActive(false);
            this.moveCoroutine = null;
        }
        //###################################################################################
    }
}