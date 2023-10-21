import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";

export interface SubscriptionDto {
	logisticCompanyId: number;
	subscriptionTypeId: number;
}

const apiAddress: string = "/Subscriptions";

export class SubscriptionsService {
	async logisticCompanyHasActiveSubscription(userId: number): Promise<boolean | null> {
		try {
			const response: AxiosResponse<boolean> = await api.get<boolean>(
				apiAddress + "/HasSubscription/" + userId,
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

	async put(subscriptionDto: SubscriptionDto): Promise<void> {
		try {
			const response: AxiosResponse<any> = await api.put<any>(apiAddress, subscriptionDto, config);
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
	}
}
