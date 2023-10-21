import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";

export interface SubscriptionType {
	subscriptionTypeId?: number;
	subscriptionTypeName: string;
	durationInDays: number;
	price: number;
}

const apiAddress: string = "/SubscriptionTypes";

export class SubscriptionTypesService {
	async getAll(): Promise<SubscriptionType[] | null> {
		try {
			const response: AxiosResponse<SubscriptionType[]> = await api.get<SubscriptionType[]>(apiAddress, config);
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

	async create(subscriptionType: SubscriptionType): Promise<void> {
		try {
			const response: any = await api.post<SubscriptionType>(apiAddress, subscriptionType, config);
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
	}
	async update(subscriptionType: SubscriptionType): Promise<void> {
		try {
			const response: any = await api.put<SubscriptionType>(apiAddress, subscriptionType, config);
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
	}

	async delete(id: number): Promise<void> {
		try {
			const response: any = await api.delete<SubscriptionType>(apiAddress + "/" + id, config);
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
	}
}
