using UnityEngine;

public class NewPPrefs : MonoBehaviour {
	
	//############################################# HasKey #############################################
	
	//A has key method for NewPPrefs
	public static bool HasKey(string key){
		string[] types = {"{0}","NewPPrefs:bool:{0}","NewPPrefs:Colour:{0}-r","NewPPrefs:Colour32:{0}-r","NewPPrefs:Vector2:{0}-x","NewPPrefs:Vector3:{0}-x","NewPPrefs:Vector4:{0}-x","NewPPrefs:Vector3:Quaternion:{0}-x","NewPPrefs:Vector4:Rect:{0}-x"};
		bool flag = false;
		foreach( string type in types ){
			if( PlayerPrefs.HasKey(string.Format(type,key)) )
				flag = true;
		}
		return flag;
	}
			
	//############################################### int ##############################################
	
	//Ints stored normally just to make things nice and similar in user projects
	public static void SetInt(string key, int value){
		PlayerPrefs.SetInt(key, value);
	}
	
	public static int GetInt(string key){
		return PlayerPrefs.GetInt(key);
	}
	
	public static int GetInt(string key, int defaultValue){
		return PlayerPrefs.GetInt(key, defaultValue);
	}
	
	//############################################### float ##############################################
	
	//Floats also stored normally just to make things nice and similar in user projects
	public static void SetFloat(string key, float value){
		PlayerPrefs.SetFloat(key, value);
	}
	
	public static float GetFloat(string key){
		return PlayerPrefs.GetFloat(key);
	}
	
	public static float GetFloat(string key, float defaultValue){
		return PlayerPrefs.GetFloat(key, defaultValue);
	}
	
	//############################################### String ##############################################
	
	//And strings
	public static void SetString(string key, string value){
		PlayerPrefs.SetString(key, value);
	}
	
	public static string GetString(string key){
		return PlayerPrefs.GetString(key);
	}
	
	public static string GetString(string key, string defaultValue){
		return PlayerPrefs.GetString(key, defaultValue);
	}
	
	//############################################## bool ##############################################
	
	//Store bool as 0 or 1
	public static void SetBool(string key, bool value){
		if( value )
			PlayerPrefs.SetInt("NewPPrefs:bool:"+key,1);
		else
			PlayerPrefs.SetInt("NewPPrefs:bool:"+key,0);
	}
	
	public static bool GetBool(string key){
		return GetBool(key,false);
	}
	
	public static bool GetBool(string key, bool defaultValue){
		int value = PlayerPrefs.GetInt("NewPPrefs:bool:"+key, 2);
		if( value == 2 )		//Return default
			return defaultValue;
		else if( value == 1 )	//Return true
			return true;
		else					//Return false
			return false;
	}
	
	//############################################## Color ##############################################
	
	//Store Color data as RGBA floats
	public static void SetColour(string key, Color value){
		PlayerPrefs.SetFloat("NewPPrefs:Colour:"+key+"-r",value.r);
		PlayerPrefs.SetFloat("NewPPrefs:Colour:"+key+"-g",value.g);
		PlayerPrefs.SetFloat("NewPPrefs:Colour:"+key+"-b",value.b);
		PlayerPrefs.SetFloat("NewPPrefs:Colour:"+key+"-a",value.a);
	}
	
	//Rebuild Color data from RGBA floats
	public static Color GetColour(string key){
		return GetColour(key,Color.clear);
	}
	
	public static Color GetColour(string key, Color defaultValue){
		Color returnValue;
		returnValue.r = PlayerPrefs.GetFloat("NewPPrefs:Colour:"+key+"-r",defaultValue.r);
		returnValue.g = PlayerPrefs.GetFloat("NewPPrefs:Colour:"+key+"-g",defaultValue.g);
		returnValue.b = PlayerPrefs.GetFloat("NewPPrefs:Colour:"+key+"-b",defaultValue.b);
		returnValue.a = PlayerPrefs.GetFloat("NewPPrefs:Colour:"+key+"-a",defaultValue.a);
		return returnValue;
	}
		
	//############################################ Color 32 #############################################
	
	//Store Color32 data RGBA Ints
	public static void SetColour32(string key, Color32 value){
		PlayerPrefs.SetInt("NewPPrefs:Colour32:"+key+"-r",value.r);
		PlayerPrefs.SetInt("NewPPrefs:Colour32:"+key+"-g",value.g);
		PlayerPrefs.SetInt("NewPPrefs:Colour32:"+key+"-b",value.b);
		PlayerPrefs.SetInt("NewPPrefs:Colour32:"+key+"-a",value.a);
	}
	
	//Rebuild Color32 data from RGBA Ints
	
	//############################################# Vector2 #############################################
	
	//Store Vector2 data as as x & y floats
	public static void SetVector2(string key, Vector2 value){
		PlayerPrefs.SetFloat("NewPPrefs:Vector2:"+key+"-x",value.x);
		PlayerPrefs.SetFloat("NewPPrefs:Vector2:"+key+"-y",value.y);
	}
	
	//Rebuild Vector2 from floats
	public static Vector2 GetVector2(string key){
		return GetVector2(key,Vector2.zero);
	}
	
	public static Vector2 GetVector2(string key, Vector2 defaultValue){
		Vector2 returnValue;
		returnValue.x = PlayerPrefs.GetFloat("NewPPrefs:Vector2:"+key+"-x",defaultValue.x);
		returnValue.y = PlayerPrefs.GetFloat("NewPPrefs:Vector2:"+key+"-y",defaultValue.y);
		return returnValue;
	}
	
	//############################################# Vector3 #############################################
	
	//Store Vector3 data as as x, y & z floats
	public static void SetVector3(string key, Vector3 value){
		PlayerPrefs.SetFloat("NewPPrefs:Vector3:"+key+"-x",value.x);
		PlayerPrefs.SetFloat("NewPPrefs:Vector3:"+key+"-y",value.y);
		PlayerPrefs.SetFloat("NewPPrefs:Vector3:"+key+"-z",value.z);
	}
	
	//Rebuild Vector3 from floats
	public static Vector3 GetVector3(string key){
		return GetVector3(key,Vector3.zero);
	}
	
	public static Vector3 GetVector3(string key, Vector3 defaultValue){
		Vector3 returnValue;
		returnValue.x = PlayerPrefs.GetFloat("NewPPrefs:Vector3:"+key+"-x",defaultValue.x);
		returnValue.y = PlayerPrefs.GetFloat("NewPPrefs:Vector3:"+key+"-y",defaultValue.y);
		returnValue.z = PlayerPrefs.GetFloat("NewPPrefs:Vector3:"+key+"-z",defaultValue.z);
		return returnValue;
	}
	
	//############################################# Vector4 #############################################
	
	//Store Vector4 data as as x, y, z & w floats
	public static void SetVector4(string key, Vector4 value){
		PlayerPrefs.SetFloat("NewPPrefs:Vector4:"+key+"-x",value.x);
		PlayerPrefs.SetFloat("NewPPrefs:Vector4:"+key+"-y",value.y);
		PlayerPrefs.SetFloat("NewPPrefs:Vector4:"+key+"-z",value.z);
		PlayerPrefs.SetFloat("NewPPrefs:Vector4:"+key+"-w",value.w);
	}
	
	//Rebuild Vector4 from floats
	public static Vector4 GetVector4(string key){
		return GetVector4(key,Vector4.zero);
	}
	
	public static Vector4 GetVector4(string key, Vector4 defaultValue){
		Vector4 returnValue;
		returnValue.x = PlayerPrefs.GetFloat("NewPPrefs:Vector4:"+key+"-x",defaultValue.x);
		returnValue.y = PlayerPrefs.GetFloat("NewPPrefs:Vector4:"+key+"-y",defaultValue.y);
		returnValue.z = PlayerPrefs.GetFloat("NewPPrefs:Vector4:"+key+"-z",defaultValue.z);
		returnValue.w = PlayerPrefs.GetFloat("NewPPrefs:Vector4:"+key+"-w",defaultValue.w);
		return returnValue;
	}
	
	//############################################ Quaternion ############################################
	
	//For simplicity we are just going to put Quaternions into Vector3s with "Quaternion" before the key
	public static void SetQuaternion(string key, Quaternion value){
		SetVector3("Quaternion:"+key,value.eulerAngles);
	}
	
	public static Quaternion GetQuaternion(string key){
		return Quaternion.Euler( GetVector3("Quaternion:"+key,Quaternion.identity.eulerAngles) );
	}
	
	public static Quaternion GetQuaternion(string key, Quaternion defaultValue){
		return Quaternion.Euler( GetVector3("Quaternion:"+key,defaultValue.eulerAngles) );
	}
	
	//############################################### Rect ###############################################
	
	//Similar to Quaternions we are just going to put Rects into Vector4s with "Rect" before the key
	public static void SetRect(string key, Rect value){
		SetVector4("Rect:"+key, new Vector4( value.x, value.y, value.width, value.height ));
	}
	
	public static Rect GetRect(string key){
		Vector4 v4 = GetVector4("Rect:"+key, Vector4.zero );
		return new Rect( v4.x, v4.y, v4.z, v4.w );
	}
	
	public static Rect GetRect(string key, Rect defaultValue){
		Vector4 v4 = GetVector4("Rect:"+key, new Vector4( defaultValue.x, defaultValue.y, defaultValue.width, defaultValue.height ) );
		return new Rect( v4.x, v4.y, v4.z, v4.w );
	}
}

public class PPP : NewPPrefs{}