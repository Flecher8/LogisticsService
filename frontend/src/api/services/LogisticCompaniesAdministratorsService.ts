import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";
import { LogisticCompany } from "./LogisticCompaniesService";

export interface LogisticCompaniesAdministratorDto {
	logisticCompaniesAdministratorId?: number;
	firstName: string;
	lastName: string;
	email: string;
	password: string;
	logisticCompanyId: number;
}

export interface LogisticCompaniesAdministrator {
	logisticCompaniesAdministratorId: number;
	firstName: string;
	lastName: string;
	email: string;
	logisticCompany: LogisticCompany;
}

const apiAddress: string = "/LogisticCompaniesAdministrators";

export class LogisticCompaniesAdministratorsService {
	async getItemByLogisticCompany(logisticCompanyId: number): Promise<LogisticCompaniesAdministratorDto[] | null> {
		try {
			const response: AxiosResponse<LogisticCompaniesAdministratorDto[]> = await api.get<
				LogisticCompaniesAdministratorDto[]
			>(apiAddress + "/logisticCompanyId/" + logisticCompanyId, config);
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

	async getItem(userId: number): Promise<LogisticCompaniesAdministrator | null> {
		try {
			const response: AxiosResponse<LogisticCompaniesAdministrator> = await api.get<LogisticCompaniesAdministrator>(
				apiAddress + "/id/" + userId,
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

	async create(item: LogisticCompaniesAdministratorDto): Promise<void> {
		try {
			const response: any = await api.post<LogisticCompaniesAdministratorDto>(apiAddress, item, config);
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
	}

	async delete(id: number): Promise<void> {
		try {
			const response: any = await api.delete<any>(apiAddress + "/id/" + id, config);
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
	}
}
