import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";

export interface Sensor {
	sensorId?: number;
	smartDeviceId: number;
}

const apiAddress: string = "/Sensors";

export class SensorsService {
	async getAll(): Promise<Sensor[] | null> {
		try {
			const response: AxiosResponse<Sensor[]> = await api.get<Sensor[]>(apiAddress, config);

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

	async create(item: Sensor): Promise<void> {
		try {
			const response: any = await api.post<Sensor>(apiAddress, item, config);
		} catch (err: any) {
			if (err.response?.status === 400) {
				throw new Error(err.response.data);
			}
		}
	}
	async update(item: Sensor): Promise<void> {
		try {
			const response: any = await api.put<Sensor>(apiAddress, item, config);
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
