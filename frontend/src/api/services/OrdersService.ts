import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";
import { PrivateCompany } from "./PrivateCompaniesService";
import { LogisticCompany } from "./LogisticCompaniesService";
import { Address } from "./AddressesService";
import { Cargo } from "./CargosService";

export interface ActiveOrders {
	waitingForAcceptanceByLogisticCompanyOrders: Order[];
	waitingForPaymentByPrivateCompanyOrders: Order[];
	acceptedOrders: Order[];
	inTransitOrders: Order[];
}

export interface OrderDto {
	orderId?: number;
	privateCompanyId: number;
	logisticCompanyId: number;
	logisticCompaniesDriverId?: number;
	sensorId?: number;
	cargoId: number;
	startDeliveryAddressId: number;
	endDeliveryAddressId: number;
	estimatedDeliveryDateTime: string;
}

export interface Order {
	orderId: number;
	privateCompany: PrivateCompany;
	logisticCompany: LogisticCompany;
	cargo: Cargo;
	startDeliveryAddress: Address;
	endDeliveryAddress: Address;
	orderStatus: OrderStatus;
	creationDateTime: string;
	estimatedDeliveryDateTime: string;
	startDeliveryDateTime?: string;
	deliveryDateTime?: string;
	price: number;
	pathLength: number;
}

export enum OrderStatus {
	WaitingForAcceptanceByLogisticCompany,
	WaitingForPaymentByPrivateCompany,
	OrderAccepted,
	PreparingForDispatch,
	InTransit,
	Delivered,
	Cancelled
}

export interface Point {
	latitude: number;
	longitude: number;
}

const apiAddress: string = "/Orders";
const privateCompanyOrdersApi = "/privateCompanyId/";

const orderTrackerApi = "/OrderTrackers/CurrentOrderTracker/orderId/";

export class OrdersService {
	async getOrderCurrentLocation(id: number): Promise<Point | null> {
		try {
			const response: AxiosResponse<Point> = await api.get<Point>(orderTrackerApi + id, config);
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

	async getOrder(id: number): Promise<Order | null> {
		try {
			const response: AxiosResponse<Order> = await api.get<Order>(apiAddress + "/id/" + id, config);
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

	async getCancelledOrders(userId: number): Promise<Order[] | null> {
		try {
			const orders: Order[] | null = await this.getOrders(userId);

			if (orders !== null) {
				const cancelledOrders: Order[] | null = this.getOrdersByOrderStatus(orders, OrderStatus.Cancelled);

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
				const deliveredOrders: Order[] | null = this.getOrdersByOrderStatus(orders, OrderStatus.Delivered);

				return deliveredOrders;
			}
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
		return null;
	}

	async getActiveOrders(userId: number): Promise<ActiveOrders | null> {
		try {
			const orders: Order[] | null = await this.getOrders(userId);
			if (orders !== null) {
				const waitingForAcceptanceByLogisticCompanyOrders: Order[] = this.getOrdersByOrderStatus(
					orders,
					OrderStatus.WaitingForAcceptanceByLogisticCompany
				);
				const waitingForPaymentByPrivateCompanyOrders: Order[] = this.getOrdersByOrderStatus(
					orders,
					OrderStatus.WaitingForPaymentByPrivateCompany
				);

				const acceptedOrders: Order[] = this.getOrdersByOrderStatus(orders, OrderStatus.OrderAccepted);

				const inTransitOrders: Order[] = this.getOrdersByOrderStatus(orders, OrderStatus.InTransit);

				const result: ActiveOrders = {
					waitingForAcceptanceByLogisticCompanyOrders: waitingForAcceptanceByLogisticCompanyOrders,
					waitingForPaymentByPrivateCompanyOrders: waitingForPaymentByPrivateCompanyOrders,
					acceptedOrders: acceptedOrders,
					inTransitOrders: inTransitOrders
				};

				return result;
			}
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
		return null;
	}

	private getOrdersByOrderStatus(orders: Order[], orderStatus: OrderStatus): Order[] {
		const result: Order[] = orders.filter(order => order.orderStatus === orderStatus);
		return result;
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

	async create(orderDto: OrderDto): Promise<void> {
		try {
			const response: AxiosResponse<any> = await api.post<any>(apiAddress, orderDto, config);
			if (response.status === 200) {
				return response.data;
			}
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
	}
}
