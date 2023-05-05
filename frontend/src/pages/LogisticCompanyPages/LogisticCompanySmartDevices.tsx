import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import { SmartDevice, SmartDevicesService } from "../../api/services/SmartDevicesService";
import { LogisticCompanyPanel } from "../../components/LogisticCompanyPanel";

// TODO Language
export const LogisticCompanySmartDevices: FC = () => {
	const [items, setItems] = useState<SmartDevice[] | null>([]);

	const getItems = async (): Promise<void> => {
		try {
			const userId: number = parseInt(localStorage["userId"]);
			const response: SmartDevice[] | null = await SmartDevicesService.prototype.getItemsByLogisticCompany(userId);
			if (response === null) {
			}
			setItems(response);
		} catch (err) {
			alert(err);
		}
	};

	useEffect(() => {
		getItems();
	}, []);
	return (
		<div className="LogisticCompanySmartDevices container">
			{/* // TODO Language */}
			<div className="d-flex border border-dark w-100">
				<LogisticCompanyPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>Your Smart Devices</h1>
					</div>
				</header>
				<div className="container mt-5">
					<p>If you want to change or buy some smart device, you can contact us by this email: email@gmail.com</p>
				</div>
				<main>
					<div className="mt-5">
						<Table className="table table-striped auto__table text-center" striped bordered hover size="lg">
							<thead>
								<tr>
									<th>Id</th>
									<th>Number Of Sensors</th>
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
										<td colSpan={5}>No data</td>
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
