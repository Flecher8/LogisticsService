import { LoginResponce } from "../helpers/viewModels/LoginResponce";
import { LoginViewModel } from "../helpers/viewModels/LoginViewModel";
import { api } from "./axios/axios";

// TODO Language
export const loginApi = async (props: LoginViewModel): Promise<LoginResponce | undefined> => {
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
