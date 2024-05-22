using System.Collections;
using System.Collections.Generic;

public abstract class Singletion<T> where T: class,new ()  {
    private static  T _Instance;
    public static  T Instance { get {
	if (_Instance == null) {
	 _Instance = new T();
	}
	
	return _Instance; } }
	
	protected Singletion() { }
}
