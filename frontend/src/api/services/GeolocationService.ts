import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";
import { Address } from "./OrdersService";

const apiAddress: string = "/Geocode?language=";

export class GeolocationService {
	async getAddress(address: Address, language: string): Promise<string | null> {
		try {
			const response: AxiosResponse<string> = await api.post<string>(apiAddress + language, address, config);
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
