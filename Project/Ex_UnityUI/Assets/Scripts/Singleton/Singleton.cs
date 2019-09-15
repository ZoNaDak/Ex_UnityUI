using System;
using System.Reflection;

namespace EX_UnityUI.Singleton {
    public abstract class Singleton<T> where T : class, new() {

        protected static volatile T instance = null;
        protected static readonly object instanceLock = new object();
    
        public static T Instance {
            get {
                lock(instanceLock) {
                    if (instance == null) {
                        instance = new T();
                    }
                }
                return instance;
            }
        }
    }
}