import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";

export interface LogisticCompaniesViewModel {
	$id: number;
	$values: LogisticCompany[];
}

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
			const response: AxiosResponse<LogisticCompaniesViewModel> = await api.get<LogisticCompaniesViewModel>(
				apiAddress,
				config
			);
			if (response.status === 200) {
				return response.data.$values;
			}
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
		return null;
	}
}
