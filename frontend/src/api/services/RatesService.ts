import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";
import { LogisticCompaniesService, LogisticCompany } from "./LogisticCompaniesService";

export interface LogisticCompanyRates {
	logisticCompanyId: number;
	logisticCompanyName: string;
	rate: number;
}

export interface Rate {
	rateId: number;
	priceForKmInDollar: number;
}

const apiAddress: string = "/Rates/logisticCompanyId/";

export class RatesService {
	async getRates(): Promise<LogisticCompanyRates[] | null> {
		try {
			const logisticCompanies: LogisticCompany[] | null = await LogisticCompaniesService.prototype.getAll();
			if (logisticCompanies === null) return null;

			let rates: LogisticCompanyRates[] = [];

			for (let i = 0; i < logisticCompanies.length; i++) {
				const rate = await this.getItemByLogisticCompany(logisticCompanies[i].logisticCompanyId);
				if (rate) {
					const logisticCompanyRate: LogisticCompanyRates = {
						logisticCompanyId: logisticCompanies[i].logisticCompanyId,
						logisticCompanyName: logisticCompanies[i].companyName,
						rate: rate.priceForKmInDollar
					};
					rates.push(logisticCompanyRate);
				}
			}

			return rates;
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
		return null;
	}

	async getItemByLogisticCompany(logisticCompanyId: number): Promise<Rate | null> {
		try {
			const response: AxiosResponse<Rate> = await api.get<Rate>(apiAddress + logisticCompanyId, config);
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
