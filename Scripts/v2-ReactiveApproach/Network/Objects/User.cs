using System.Collections.Generic;

[System.Serializable]
public class User {

	public int id;
	public string first_name;
	public string last_name;
	public string avatar;

}

[System.Serializable]
public class GetListUsersResponse {

	public int page;
	public int per_page;
	public int total;
	public int total_pages;
	public List<User> data;

}

[System.Serializable]
public class GetSingleUserResponse {

	public User data;

}