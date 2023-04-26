import { getUserIdApi } from "../../api/getUserIdApi";
import { loginApi } from "../../api/loginApi";
import { LoginResponce } from "../viewModels/LoginResponce";
import { LoginViewModel } from "../viewModels/LoginViewModel";

export default class LoginService {
	async login(email: string, password: string): Promise<void> {
		const loginViewModel: LoginViewModel = {
			email: email,
			password: password
		};
		const response: LoginResponce | undefined = await loginApi(loginViewModel);
		if (response === undefined) {
			return;
		}

		this.saveUserLoginData(response);
		this.saveUserEmail(email);

		const id: number | undefined = await getUserIdApi(email);
		if (id === undefined) {
			return;
		}

		this.saveUserId(id);
		this.loginRedirect(response);
	}

	loginRedirect = (loginResponse: LoginResponce | null): void => {
		// Set default location to "/Login"
		let newLocation = "/Login";

		// If a valid login response was provided, determine the appropriate new location based on the user type
		if (loginResponse !== null) {
			switch (loginResponse.userType) {
				case "PrivateCompany":
					newLocation = "/PrivateCompanyProfile";
					break;
				case "LogisticCompany":
					newLocation = "/LogisticCompanyProfile";
					break;
				case "LogisticCompanyAdministrator":
					newLocation = "/LogisticCompanyAdministratorProfile";
					break;
				case "SystemAdmin":
					newLocation = "/SystemAdminProfile";
					break;
			}
		}

		// Redirect to the new location
		window.location.href = newLocation;
	};

	saveUserLoginData = (loginResponse: LoginResponce | null): void => {
		if (loginResponse === null) {
			return;
		}
		localStorage.setItem("userToken", loginResponse.token);
		localStorage.setItem("userType", loginResponse.userType);
	};

	saveUserEmail = (email: string): void => {
		localStorage.setItem("userEmail", email);
	};

	saveUserId = (id: number): void => {
		localStorage.setItem("userId", id.toString());
	};
}
