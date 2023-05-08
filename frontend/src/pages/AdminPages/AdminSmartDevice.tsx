import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import { AdminPanel } from "../../components/AdminPanel";
import { SmartDevice, SmartDevicesService } from "../../api/services/SmartDevicesService";
import { CreateSmartDevice } from "../../components/AdminSmartDeviceComponents/CreateSmartDevice";
import { UpdateSmartDevice } from "../../components/AdminSmartDeviceComponents/UpdateSmartDevice";
import { useTranslationHelper } from "../../helpers/translation/translationService";

export const AdminSmartDevice: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	const [items, setItems] = useState<SmartDevice[] | null>([]);
	const [item, setItem] = useState<SmartDevice | undefined>();

	// Create modal show
	const [createItemModelShow, SetCreateItemModelShow] = useState(false);
	const createItemModelHandleClose = () => SetCreateItemModelShow(false);
	const createItemModelHandleShow = () => SetCreateItemModelShow(true);

	// Update modal show
	const [updateItemModelShow, SetUpdateItemModelShow] = useState(false);
	const updateItemModelHandleClose = () => SetUpdateItemModelShow(false);
	const updateItemModelHandleShow = () => SetUpdateItemModelShow(true);

	// Show add modal function
	function addModalShow() {
		createItemModelHandleShow();
	}

	function handleEditItem(id: number | undefined) {
		setItem(items?.find(e => e.smartDeviceId === id));
		updateItemModelHandleShow();
	}

	async function handleDeleteItem(id: number | undefined) {
		const confirmationText = t("Are you sure?") ?? "";
		if (window.confirm(confirmationText) && id !== undefined) {
			try {
				await SmartDevicesService.prototype.delete(id);
				document.location.reload();
			} catch (err: any) {
				// errors that expected from back
				alert(err);
			}
		}
	}

	const getItems = async (): Promise<void> => {
		try {
			const response: SmartDevice[] | null = await SmartDevicesService.prototype.getAll();
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
		<div className="AdminSensor container">
			<div className="d-flex border border-dark w-100">
				<AdminPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>{t("Smart Devices")}</h1>
					</div>
				</header>
				<div className="container mt-5">
					<Button onClick={() => addModalShow()} variant="outline-primary">
						{t("Create")}
					</Button>
				</div>
				<Modal size="lg" centered show={createItemModelShow} onHide={createItemModelHandleClose}>
					<CreateSmartDevice close={createItemModelHandleClose} />
				</Modal>

				<Modal size="lg" centered show={updateItemModelShow} onHide={updateItemModelHandleClose}>
					<UpdateSmartDevice close={updateItemModelHandleClose} item={item} />
				</Modal>
				<main>
					<div className="mt-5">
						<Table className="table table-striped auto__table text-center" striped bordered hover size="lg">
							<thead>
								<tr>
									<th>{t("Id")}</th>
									<th>{t("Logistic Company Id")}</th>
									<th>{t("Number of Sensors")}</th>
								</tr>
							</thead>
							<tbody>
								{items ? (
									items.map(e => (
										<tr key={e.smartDeviceId}>
											<td>{e.smartDeviceId}</td>
											<td>{e.logisticCompanyId}</td>
											<td>{e.numberOfSensors}</td>
											<td>
												<Button onClick={() => handleEditItem(e.smartDeviceId)} variant="outline-dark">
													{t("Update")}
												</Button>
												<Button onClick={() => handleDeleteItem(e.smartDeviceId)} variant="outline-danger">
													{t("Delete")}
												</Button>
											</td>
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
