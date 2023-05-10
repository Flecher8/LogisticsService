import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";

export interface SmartDevice {
	smartDeviceId?: number;
	logisticCompanyId: number;
	numberOfSensors: number;
}

const apiAddress: string = "/SmartDevices";

export class SmartDevicesService {
	async getAll(): Promise<SmartDevice[] | null> {
		try {
			const response: AxiosResponse<SmartDevice[]> = await api.get<SmartDevice[]>(apiAddress, config);
			if (response.status === 200) {
				return response.data;
			}
		} catch (err: any) {
			if (err.response?.status === 400) {
				console.log(err);
				throw new Error(err.response.data);
			}
		}
		return null;
	}

	async getItemsByLogisticCompany(logisticCompanyId: number): Promise<SmartDevice[] | null> {
		try {
			const response: AxiosResponse<SmartDevice[]> = await api.get<SmartDevice[]>(
				apiAddress + "/logisticCompanyId/" + logisticCompanyId,
				config
			);
			if (response.status === 200) {
				return response.data;
			}
		} catch (err: any) {
			if (err.response?.status === 400) {
				console.log(err);
				throw new Error(err.response.data);
			}
		}
		return null;
	}

	async create(item: SmartDevice): Promise<void> {
		try {
			const response: any = await api.post<SmartDevice>(apiAddress, item, config);
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
	}
	async update(item: SmartDevice): Promise<void> {
		try {
			const response: any = await api.put<SmartDevice>(apiAddress, item, config);
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
	}

	async delete(id: number): Promise<void> {
		try {
			const response: any = await api.delete<any>(apiAddress + "/id/" + id, config);
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
	}
}
