using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class NetworkHelper {

	private Dictionary<string, string> defaultHeader;

	public NetworkHelper() {
		defaultHeader = new Dictionary<string, string>();
		defaultHeader.Add("Content-Type" , "application/json");
	}

	public byte[] ConvertPostBodyToByteArray(object body) {
		string bodyString = JsonUtility.ToJson(body);
		return Encoding.ASCII.GetBytes(bodyString);
	}

	public Dictionary<string, string> GetDefaultPostHeader() {
		return defaultHeader;
	}

	public T ConvertStringToJsonObject<T>(string jsonString) {
		return JsonUtility.FromJson<T>(jsonString);
	}

}

