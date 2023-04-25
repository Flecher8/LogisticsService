import { LoginResponce } from "../helpers/viewModels/LoginResponce";
import { LoginViewModel } from "../helpers/viewModels/LoginViewModel";
import { api } from "./axios/axios";
// TODO Language
export const loginApi = async (props: LoginViewModel): Promise<boolean> => {
	try {
		const response = await api.post<LoginResponce>("/Authentication/Login", props);
		if (response.status === 200) {
			localStorage.setItem("userType", response.data.userType);
			localStorage.setItem("userToken", response.data.token);
			// TODO Move user to another page
			// if (localStorage.getItem("PeopleTracker-userType") === "User") {
			// 	window.location.href = "/UserProfile";
			// }
			// if (localStorage.getItem("PeopleTracker-userType") === "Admin") {
			// 	window.location.href = "/AdminPanelPlacements";
			// }
			console.log(localStorage);
			alert("200");
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
