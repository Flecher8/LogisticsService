import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";
import { SmartDevice } from "./SmartDevicesService";

export interface SensorDto {
	sensorId?: number;
	smartDeviceId: number;
}

export interface Sensor {
	sensorId: number;
	smartDevice: SmartDevice;
}

const apiAddress: string = "/Sensors";

export class SensorsService {
	async getAll(): Promise<SensorDto[] | null> {
		try {
			const response: AxiosResponse<SensorDto[]> = await api.get<SensorDto[]>(apiAddress, config);

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

	async getItemsBySmartDevice(smartDeviceId: number): Promise<Sensor[] | null> {
		try {
			const response: AxiosResponse<Sensor[]> = await api.get<Sensor[]>(
				apiAddress + "/smartDeviceId/" + smartDeviceId,
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

	async create(item: SensorDto): Promise<void> {
		try {
			const response: any = await api.post<SensorDto>(apiAddress, item, config);
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
	}
	async update(item: SensorDto): Promise<void> {
		try {
			const response: any = await api.put<SensorDto>(apiAddress, item, config);
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
