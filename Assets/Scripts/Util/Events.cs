using UnityEngine;
using System.Collections.Generic;

public class Events{
    private static EventCache ec = new EventCache();

    public static bool on(string name){
        return ec.on(name);
    }
    public static void call(string name){
        ec.call (name);
    }

    public class EventCache : Dictionary<string, bool>{
        public bool on(string name){
            try{
                bool value = this[name];
                if(value) this[name] = false;
                return value;
            }catch(KeyNotFoundException){
                return false;
            }
        }
        public void call(string name){
            try{
                this[name] = true;
            }catch(KeyNotFoundException){
                this.Add (name, true);
            }
        }
    }
}

