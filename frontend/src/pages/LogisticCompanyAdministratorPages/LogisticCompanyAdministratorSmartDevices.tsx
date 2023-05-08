import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import { SmartDevice, SmartDevicesService } from "../../api/services/SmartDevicesService";
import { LogisticCompaniesAdministratorsService } from "../../api/services/LogisticCompaniesAdministratorsService";
import { LogisticCompaniesAdministratorPanel } from "../../components/LogisticCompaniesAdministratorPanel";
import { useTranslationHelper } from "../../helpers/translation/translationService";

export const LogisticCompanyAdministratorSmartDevices: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	const [items, setItems] = useState<SmartDevice[] | null>([]);

	const getItems = async (): Promise<void> => {
		try {
			const logisticCompanyId: number =
				await LogisticCompaniesAdministratorsService.prototype.getLogisticCompanyId();
			const response: SmartDevice[] | null = await SmartDevicesService.prototype.getItemsByLogisticCompany(
				logisticCompanyId
			);
			if (response === null) return;

			setItems(response);
		} catch (err) {
			alert(err);
		}
	};

	useEffect(() => {
		getItems();
	}, []);
	return (
		<div className="LogisticCompanyAdministratorSmartDevices container">
			<div className="d-flex border border-dark w-100">
				<LogisticCompaniesAdministratorPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>{t("Your Logistic company Smart Devices")}</h1>
					</div>
				</header>
				<div className="container mt-5">
					<p>{t("If you want to change some smart device, you can contact your logistic company")}</p>
				</div>
				<main>
					<div className="mt-5">
						<Table className="table table-striped auto__table text-center" striped bordered hover size="lg">
							<thead>
								<tr>
									<th>{t("Id")}</th>
									<th>{t("Number Of Sensors")}</th>
								</tr>
							</thead>
							<tbody>
								{items ? (
									items.map(e => (
										<tr key={e.smartDeviceId}>
											<td>{e.smartDeviceId}</td>
											<td>{e.numberOfSensors}</td>
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
