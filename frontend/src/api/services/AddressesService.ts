import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";

export interface Address {
	addressId?: number;
	addressName: string;
	latitude: number;
	longitute: number;
}

const apiAddress: string = "/Addresses";

export class AddressesService {
	async create(address: Address): Promise<Address | null> {
		try {
			const response: AxiosResponse<Address> = await api.post<Address>(apiAddress, address, config);
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
