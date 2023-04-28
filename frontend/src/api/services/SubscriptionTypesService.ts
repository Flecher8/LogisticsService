import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";

export interface SubscritionTypesViewModel {
	$id: number;
	$values: SubscriptionType[];
}

export interface SubscriptionType {
	subscriptionTypeId?: number;
	subscriptionTypeName: string;
	durationInDays: number;
	price: number;
}

export class SubscriptionTypesService {
	async getSubscriptionTypes(): Promise<SubscriptionType[] | null> {
		try {
			const response: AxiosResponse<SubscritionTypesViewModel> = await api.get<SubscritionTypesViewModel>(
				"/SubscriptionTypes",
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

	async createSubscriptionType(subscriptionType: SubscriptionType): Promise<SubscriptionType | null> {
		return null;
	}
	async updateSubscriptionType(subscriptionType: SubscriptionType): Promise<SubscriptionType | null> {
		return null;
	}

	async deleteSubscriptionType(id: number): Promise<void> {}
}
