import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";

export interface CancelledOrder {
	orderId: number;
	reason: string;
	description: string;
	cancelledBy: string;
	cancelledById: number;
}

const apiAddress: string = "/CancelledOrders";

export class CancelledOrderService {
	async cancelOrder(cancelledOrder: CancelledOrder): Promise<void> {
		try {
			const response: AxiosResponse<any> = await api.post<any>(apiAddress, cancelledOrder, config);
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
	}
}
