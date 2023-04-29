import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";

export interface SmartDeviceViewModel {
	$id: number;
	$values: SmartDevice[];
}

export interface SmartDevice {}

export class SmartDevicesService {
	async getAll(): Promise<SmartDevice[] | null> {
		return null;
	}

	async create(subscriptionType: SmartDevice): Promise<void> {}
	async update(subscriptionType: SmartDevice): Promise<void> {}

	async delete(id: number): Promise<void> {}
}
