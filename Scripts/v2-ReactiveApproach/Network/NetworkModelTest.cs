using System;
using UniRx;

public class NetworkModelTest {

	private Endpoint endpoint;
	private NetworkHelper networkHelper;

	private CompositeDisposable compositeDisposable;
	private float callDelay = 1f;

	public NetworkModelTest() {
		endpoint = new Endpoint();
		networkHelper = new NetworkHelper();
		compositeDisposable = new CompositeDisposable();
	}

	public void GetUser_Success() {
		GetUser(2);
	}

	public void GetUser_Failure() {
		GetUser(23);
	}

	private void GetUser(int userId) {
		IDisposable query = ObservableWWW
			.Get(endpoint.GetEndpointGetUser(userId))
			.DelaySubscription(System.DateTimeOffset.Now.Add(TimeSpan.FromSeconds(callDelay)))
//			.CatchIgnore((WWWErrorException error) => 
//				LogUtil.PrintWarning(GetType(), "GetUser() CATCH IGNORE " + error.Message))
			.Subscribe(
				userString => {
					GetSingleUserResponse userObject = 
						networkHelper.ConvertStringToJsonObject<GetSingleUserResponse>(userString);
					
					LogUtil.PrintInfo(GetType(), "GetUser() the user is: " 
						+ userObject.data.first_name + " " + userObject.data.last_name + 
						" with ID: " + userObject.data.id + " & avatar " + userObject.data.avatar);
				},
				error => LogUtil.PrintWarning(GetType(), "GetUser() " + error.Message)
			);

		compositeDisposable.Add(query);
	}

	public void PostLogin_Success() {
		PostLoginBody successBody = new PostLoginBody();
		successBody.email = "peter@klaven";
		successBody.password = "cityslicka";

		PostLogin(successBody);
	}

	public void PostLogin_Failure() {
		PostLoginBody failureBody = new PostLoginBody();
		failureBody.email = "peter@klaven";

		PostLogin(failureBody);
	}

	private void PostLogin(PostLoginBody body) {
		IDisposable query = ObservableWWW.Post(endpoint.GetEndpointPostLogin(), 
							networkHelper.ConvertPostBodyToByteArray(body), 
							networkHelper.GetDefaultPostHeader(), null)
			.DelaySubscription(System.DateTimeOffset.Now.Add(TimeSpan.FromSeconds(callDelay)))
			.CatchIgnore((WWWErrorException error) => {
				GenericErrorResponse errorResponse = 
					networkHelper.ConvertStringToJsonObject<GenericErrorResponse>(error.Text);
				LogUtil.PrintWarning(GetType(), "PostLogin() CATCH IGNORE GenericErrorResponse: " + errorResponse.error);
			})
			.Subscribe(
				tokenString => {
					Token tokenObject = networkHelper.ConvertStringToJsonObject<Token>(tokenString);
					LogUtil.PrintInfo(GetType(), "PostLogin() " + tokenObject.token);
				}
//				, 
//				error => { 
//					LogUtil.PrintWarning(GetType(), "PostLogin() " + error.Message); 
//
//					GenericErrorResponse errorResponse = JsonUtility.FromJson<GenericErrorResponse>((error as WWWErrorException).Text);
//					LogUtil.PrintWarning(GetType(), "PostLogin() CATCH IGNORE GenericErrorResponse: " + errorResponse.error);
//				}
			);

		compositeDisposable.Add(query);
	}

}
