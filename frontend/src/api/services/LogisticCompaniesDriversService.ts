import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";
import { LogisticCompany } from "./LogisticCompaniesService";

export interface LogisticCompaniesDriverDto {
	id?: number;
	firstName: string;
	lastName: string;
	email: string;
	password: string;
	logisticCompanyId: number;
}

export interface LogisticCompaniesDriver {
	logisticCompaniesDriverId: number;
	firstName: string;
	lastName: string;
	email: string;
	logisticCompany: LogisticCompany;
}

const apiAddress: string = "/LogisticCompaniesDrivers";

export class LogisticCompaniesDriversService {
	async getItemsByLogisticCompany(logisticCompanyId: number): Promise<LogisticCompaniesDriver[] | null> {
		try {
			const response: AxiosResponse<LogisticCompaniesDriver[]> = await api.get<LogisticCompaniesDriver[]>(
				apiAddress + "/logisticCompanyId/" + logisticCompanyId,
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

	async getItem(userId: number): Promise<LogisticCompaniesDriver | null> {
		try {
			const response: AxiosResponse<LogisticCompaniesDriver> = await api.get<LogisticCompaniesDriver>(
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

	async create(item: LogisticCompaniesDriverDto): Promise<void> {
		try {
			const response: any = await api.post<any>(apiAddress, item, config);
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
