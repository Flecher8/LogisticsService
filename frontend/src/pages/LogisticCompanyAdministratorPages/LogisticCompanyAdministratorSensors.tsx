import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import { SmartDevice, SmartDevicesService } from "../../api/services/SmartDevicesService";
import { LogisticCompaniesAdministratorsService } from "../../api/services/LogisticCompaniesAdministratorsService";
import { Sensor, SensorsService } from "../../api/services/SensorsService";
import { LogisticCompaniesAdministratorPanel } from "../../components/LogisticCompaniesAdministratorPanel";
import { useTranslationHelper } from "../../helpers/translation/translationService";

export const LogisticCompanyAdministratorSensors: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	const [items, setItems] = useState<Sensor[] | null>([]);

	const getItems = async (): Promise<void> => {
		try {
			const logisticCompanyId: number =
				await LogisticCompaniesAdministratorsService.prototype.getLogisticCompanyId();
			const smartDevices: SmartDevice[] | null = await SmartDevicesService.prototype.getItemsByLogisticCompany(
				logisticCompanyId
			);
			if (smartDevices === null) return;

			let sensors: Sensor[] = [];

			for (const smartDevice of smartDevices) {
				if (smartDevice.smartDeviceId !== undefined) {
					const sensorsBySmartDevice: Sensor[] | null = await SensorsService.prototype.getItemsBySmartDevice(
						smartDevice.smartDeviceId
					);
					if (sensorsBySmartDevice !== null) {
						sensors = sensors.concat(sensorsBySmartDevice);
					}
				}
			}

			setItems(sensors);
		} catch (err) {
			alert(err);
		}
	};

	useEffect(() => {
		getItems();
	}, []);
	return (
		<div className="LogisticCompanyAdministratorSensors container">
			<div className="d-flex border border-dark w-100">
				<LogisticCompaniesAdministratorPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>{t("Your Logistic company sensors")}</h1>
					</div>
				</header>
				<div className="container mt-5">
					<p>{t("If you want to change some sensor, you can contact your logistic company")}</p>
				</div>
				<main>
					<div className="mt-5">
						<Table className="table table-striped auto__table text-center" striped bordered hover size="lg">
							<thead>
								<tr>
									<th>{t("Id")}</th>
									<th>{t("Smart device id")}</th>
								</tr>
							</thead>
							<tbody>
								{items ? (
									items.map(e => (
										<tr key={e.sensorId}>
											<td>{e.sensorId}</td>
											<td>{e.smartDevice.smartDeviceId}</td>
										</tr>
									))
								) : (
									<tr>
										<td colSpan={5}>{t("No data")}</td>
									</tr>
								)}
							</tbody>
						</Table>
					</div>
				</main>
			</div>
		</div>
	);
};
