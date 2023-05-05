import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";

const apiAddress: string = "/Payment/orderId/";

export class PaymentService {
	async payForOrder(orderId: number): Promise<void> {
		try {
			const response: AxiosResponse<any> = await api.post<any>(apiAddress + orderId, null, config);
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
	}
}
