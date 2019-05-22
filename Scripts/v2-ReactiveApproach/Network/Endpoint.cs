using UnityEngine;

public class Endpoint {

	private string BASE_URL = "";

	private string ENDPOINT_POST_LOGIN = "/api/login";
	private string ENDPOINT_GET_USER = "/api/users/";

	public Endpoint() {
		RemoteSettings.ForceUpdate();
		RemoteSettings.Updated += new RemoteSettings.UpdatedEventHandler(OnUpdateRemoteSettings);
	}

	private void OnUpdateRemoteSettings() {
		BASE_URL = RemoteSettings.GetString("Network_BaseUrl_Test", "");
		LogUtil.PrintWarning(GetType(), "OnUpdateRemoteSettings() BASE url is: " + BASE_URL);
	}

	public string GetEndpointPostLogin() {
		LogUtil.PrintWarning(GetType(), "OnUpdateRemoteSettings() " + string.Concat(BASE_URL, ENDPOINT_POST_LOGIN));
		return string.Concat(BASE_URL, ENDPOINT_POST_LOGIN);
	}

	public string GetEndpointGetUser(int userId) {
		LogUtil.PrintWarning(GetType(), "OnUpdateRemoteSettings() " + string.Concat(BASE_URL, ENDPOINT_GET_USER, userId.ToString()));
		return string.Concat(BASE_URL, ENDPOINT_GET_USER, userId.ToString());
	}

}

