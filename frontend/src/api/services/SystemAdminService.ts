import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";

export interface SystemAdmin {
	systemAdminId: number;
	firstName: string;
	lastName: string;
	email: string;
}

const apiAddress: string = "/SystemAdmins";

export class SystemAdminService {
	async getProfileInfo(id: number): Promise<SystemAdmin | null> {
		try {
			const response: AxiosResponse<SystemAdmin> = await api.get<SystemAdmin>(apiAddress + "/id/" + id, config);
			if (response.status === 200) {
				return response.data;
			}
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
		return null;
	}
}
