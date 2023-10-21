import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";

const apiAddress: string = "/Analytics";
const averageDeliveryTimeApi = "/AverageDeliveryTimeByPrivateCompany?privateCompanyId=";
const averageDeliveryPathLengthApi = "/AverageDeliveryPathLengthByPrivateCompany?privateCompanyId=";
const numberOfDeliveredOrdersApi = "/NumberOfDeliveredOrdersByPrivateCompany?privateCompanyId=";
const numberOfNotDeliveredOrdersApi = "/NumberOfNotDeliveredOrdersByPrivateCompany?privateCompanyId=";
const averageOrderPriceApi = "/AverageOrderPriceByPrivateCompany?privateCompanyId=";

export class PrivateCompanyStatisticsService {
	async getAverageDeliveryTime(userId: number): Promise<string> {
		try {
			const response: AxiosResponse<string> = await api.get<string>(
				apiAddress + averageDeliveryTimeApi + userId,
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
		return "0";
	}

	async getAverageDeliveryPathLength(userId: number, metric: string): Promise<number> {
		try {
			const response: AxiosResponse<number> = await api.get<number>(
				apiAddress + averageDeliveryPathLengthApi + userId + "&metric=" + metric,
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
		return 0;
	}

	async getNumberOfDeliveredOrders(userId: number): Promise<number> {
		try {
			const response: AxiosResponse<number> = await api.get<number>(
				apiAddress + numberOfDeliveredOrdersApi + userId,
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
		return 0;
	}

	async getNumberOfNotDeliveredOrders(userId: number): Promise<number> {
		try {
			const response: AxiosResponse<number> = await api.get<number>(
				apiAddress + numberOfNotDeliveredOrdersApi + userId,
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
		return 0;
	}

	async getAverageOrderPrice(userId: number): Promise<number> {
		try {
			const response: AxiosResponse<number> = await api.get<number>(
				apiAddress + averageOrderPriceApi + userId,
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
		return 0;
	}
}
