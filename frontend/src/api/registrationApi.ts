import { RegistrationViewModel } from "./viewModels/RegistrationViewModel";
import { api } from "./axios/axios";

// TODO Language
export const registrationApi = async (props: RegistrationViewModel): Promise<boolean> => {
	try {
		const response = await api.post<boolean>("/Authentication/Registration", props);
		if (response.status === 200) {
			return true;
		}
	} catch (err: any) {
		// errors that expected from back
		if (err.response?.status === 400) {
			alert(err.response.data);
		}
		return false;
	}
	return false;
};
