using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EX_UnityUI.Singleton {
    public abstract class MonoSingleton<T> : MonoBehaviour  where T : MonoBehaviour {
        private static T instance = null;
        private static object syncobj = new object();
        private static bool appClosing = false;

        public static T Instance {
            get {
                if(appClosing)
                    return null;
                lock(syncobj) {
                    if(instance == null) {
                        T[] objs = FindObjectsOfType<T>();

                        if(objs.Length > 1) {
                            Debug.LogError(string.Format("There is more than one {0} in the scene.",  typeof(T).Name));
                        } else if(objs.Length > 0) {
                            instance = objs[0];
                        } else if(instance == null) {
                            string objName = typeof(T).ToString();
                            GameObject obj = GameObject.Find(objName);
                            if(obj == null) {
                                obj = new GameObject(objName);
                            }
                            instance = obj.AddComponent<T>();
                        }
                    }

                    return instance;
                }
            }
        }

        protected virtual void OnApplicationQuit() {
            appClosing = true;
        }
    }
}