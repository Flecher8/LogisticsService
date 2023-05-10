import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";

export interface PrivateCompaniesViewModel {
	$id: number;
	$values: PrivateCompany[];
}

export interface PrivateCompany {
	privateCompanyId: number;
	companyName: string;
	email: string;
	description?: string;
}

const apiAddress: string = "/PrivateCompanies";

export class PrivateCompaniesService {
	async getItem(id: number): Promise<PrivateCompany | null> {
		try {
			const response: AxiosResponse<PrivateCompany> = await api.get<PrivateCompany>(
				apiAddress + "/id/" + id,
				config
			);
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
