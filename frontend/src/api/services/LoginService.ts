import { LoginResponce } from "../viewModels/LoginResponce";
import { LoginViewModel } from "../viewModels/LoginViewModel";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";

export default class LoginService {
	async login(email: string, password: string): Promise<void> {
		const loginViewModel: LoginViewModel = {
			email: email,
			password: password
		};
		const response: LoginResponce | undefined = await this.loginApi(loginViewModel);
		if (response === undefined) {
			return;
		}

		this.saveUserLoginData(response);
		this.saveUserEmail(email);

		const id: number | undefined = await this.getUserIdApi(email);
		if (id === undefined) {
			return;
		}

		this.saveUserId(id);
		this.loginRedirect(response);
	}

	private loginRedirect = (loginResponse: LoginResponce | null): void => {
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

	private saveUserLoginData = (loginResponse: LoginResponce | null): void => {
		if (loginResponse === null) {
			return;
		}
		localStorage.setItem("userToken", loginResponse.token);
		localStorage.setItem("userType", loginResponse.userType);
	};

	private saveUserEmail = (email: string): void => {
		localStorage.setItem("userEmail", email);
	};

	private saveUserId = (id: number): void => {
		localStorage.setItem("userId", id.toString());
	};

	private getUserIdApi = async (email: string): Promise<number | undefined> => {
		try {
			if (localStorage.getItem("userType") === null) {
				return undefined;
			}
			const userType: string | null = localStorage.getItem("userType");
			let url = "";
			switch (userType) {
				case "SystemAdmin":
					url = "/SystemAdmins/email/";
					break;
				case "PrivateCompany":
					url = "/PrivateCompanies/email/";
					break;
				case "LogisticCompany":
					url = "/LogisticCompanies/email/";
					break;
				case "LogisticCompanyAdministrator":
					url = "/LogisticCompaniesAdministrators/email/";
					break;
				default:
					return undefined;
			}
			const response = await api.get<any>(url + email, config);
			if (response.status === 200) {
				switch (userType) {
					case "SystemAdmin":
						return response.data.systemAdminId;
					case "PrivateCompany":
						return response.data.privateCompanyId;
					case "LogisticCompany":
						return response.data.logisticCompanyId;
					case "LogisticCompanyAdministrator":
						return response.data.logisticCompanyAdministratorId;
					default:
						return undefined;
				}
			}
		} catch (err: any) {
			// errors that expected from back
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
		return undefined;
	};

	private loginApi = async (props: LoginViewModel): Promise<LoginResponce | undefined> => {
		try {
			const response = await api.post<LoginResponce>("/Authentication/Login", props);
			if (response.status === 200) {
				return response.data;
			}
		} catch (err: any) {
			// errors that expected from back
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
		return undefined;
	};
}
