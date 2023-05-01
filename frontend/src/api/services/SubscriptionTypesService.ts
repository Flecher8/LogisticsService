import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";

export interface SubscriptionType {
	subscriptionTypeId?: number;
	subscriptionTypeName: string;
	durationInDays: number;
	price: number;
}

export class SubscriptionTypesService {
	async getAll(): Promise<SubscriptionType[] | null> {
		try {
			const response: AxiosResponse<SubscriptionType[]> = await api.get<SubscriptionType[]>(
				"/SubscriptionTypes",
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

	async create(subscriptionType: SubscriptionType): Promise<void> {
		try {
			const response: any = await api.post<SubscriptionType>("/SubscriptionTypes", subscriptionType, config);
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
	}
	async update(subscriptionType: SubscriptionType): Promise<void> {
		try {
			const response: any = await api.put<SubscriptionType>("/SubscriptionTypes", subscriptionType, config);
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
	}

	async delete(id: number): Promise<void> {
		try {
			const response: any = await api.delete<SubscriptionType>("/SubscriptionTypes/" + id, config);
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
	}
}
