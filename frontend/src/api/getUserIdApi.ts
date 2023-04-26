import { api } from "./axios/axios";
import { config } from "./userConfig";

// TODO Language
export const getUserIdApi = async (email: string): Promise<number | undefined> => {
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
