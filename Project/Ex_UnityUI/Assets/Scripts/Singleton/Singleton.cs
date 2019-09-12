using System;
using System.Reflection;

namespace EX_UnityUI.Singleton {
    public abstract class Singleton<T> where T : class {
        private static volatile T instance = null;
        private static object syncobj = new object();
    
        public static T Instance {
            get {
                if (instance == null) {
                    CreateInstance();
                }
                return instance;
            }
        }
    
        private static void CreateInstance() {
            lock(syncobj) {
                if(instance == null) {
                    Type t = typeof(T);
    
                    ConstructorInfo[] ctors = t.GetConstructors();
                    if(ctors.Length > 0) {
                        throw new InvalidOperationException(
                            string.Format(
                                "{0} has at least one accesible ctor making it impossible to enforce singleton behaviour", t.Name));
                    }
    
                    instance = (T)Activator.CreateInstance(t, true);
                }
            }
        }
    }
}