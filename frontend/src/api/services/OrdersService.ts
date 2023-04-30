import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";

export enum OrderStatus {
	WaitingForAcceptanceByLogisticCompany,
	WaitingForPaymentByPrivateCompany,
	OrderAccepted,
	PreparingForDispatch,
	InTransit,
	Delivered,
	Cancelled
}

export interface Order {
	orderId: number;
	orderStatus: OrderStatus;
	creationDateTime: string;
	estimatedDeliveryDateTime: string;
	startDeliveryDateTime?: string;
	deliveryDateTime?: string;
	price: number;
	pathLength: number;
}

// interface OrderResponce {
// 	$id: string;
// 	$values: Order[];
// }

const apiAddress: string = "/Orders";
const privateCompanyOrdersApi = "/privateCompanyId/";

export class OrdersService {
	async getCancelledOrders(userId: number): Promise<Order[] | null> {
		try {
			const orders: Order[] | null = await this.getOrders(userId);

			if (orders !== null) {
				const cancelledOrders: Order[] | null = orders.filter(order => order.orderStatus === OrderStatus.Cancelled);

				return cancelledOrders;
			}
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
		return null;
	}

	async getDeliveredOrders(userId: number): Promise<Order[] | null> {
		try {
			const orders: Order[] | null = await this.getOrders(userId);
			if (orders !== null) {
				const deliveredOrders: Order[] | null = orders.filter(order => order.orderStatus === OrderStatus.Delivered);

				return deliveredOrders;
			}
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
		return null;
	}

	private async getOrders(userId: number): Promise<Order[] | null> {
		try {
			const response: AxiosResponse<Order[]> = await api.get<Order[]>(
				apiAddress + privateCompanyOrdersApi + userId,
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
}
