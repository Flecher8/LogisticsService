export class DataTimeService {
	getLocalDataByUTCData(data: string): string {
		const utcDate = new Date(data);
		const localDate = new Date(utcDate.getTime() - utcDate.getTimezoneOffset() * 60 * 1000);
		return localDate.toLocaleString();
	}
}
