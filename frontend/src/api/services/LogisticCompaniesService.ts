import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";

export interface LogisticCompany {
	logisticCompanyId: number;
	companyName: string;
	email: string;
	description: string;
}

const apiAddress: string = "/LogisticCompanies";

export class LogisticCompaniesService {
	async getAll(): Promise<LogisticCompany[] | null> {
		try {
			const response: AxiosResponse<LogisticCompany[]> = await api.get<LogisticCompany[]>(apiAddress, config);
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
	async getItem(id: number): Promise<LogisticCompany | null> {
		try {
			const response: AxiosResponse<LogisticCompany> = await api.get<LogisticCompany>(
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
