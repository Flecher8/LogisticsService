import { AxiosResponse } from "axios";
import { api } from "../axios/axios";
import { config } from "../configuration/userConfig";

export interface CargoDto {
	cargoId?: number;
	name: string;
	weight: number;
	length: number;
	width: number;
	height: number;
	description: string;
	weightMeasureUnit: string;
	sizeMeasureUnit: string;
}

export interface Cargo {
	cargoId: number;
	name: string;
	weight: number;
	length: number;
	width: number;
	height: number;
	description: string;
}

const apiAddress: string = "/Cargos";

export class CargosService {
	async create(cargoDto: CargoDto): Promise<number | null> {
		try {
			const response: AxiosResponse<number> = await api.post<number>(apiAddress, cargoDto, config);
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
