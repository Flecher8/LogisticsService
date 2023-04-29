import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import { Link } from "react-router-dom";
import { AdminPanel } from "../../components/AdminPanel";
import { Sensor, SensorsService } from "../../api/services/SensorsService";
import { CreateSensor } from "../../components/AdminSensorComponents/CreateSensor";
import { UpdateSensor } from "../../components/AdminSensorComponents/UpdateSensor";

// TODO Language
export const AdminSensor: FC = () => {
	const [items, setItems] = useState<Sensor[] | null>([]);
	const [item, setItem] = useState<Sensor | undefined>();

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
		setItem(items?.find(e => e.sensorId === id));
		updateItemModelHandleShow();
	}

	async function handleDeleteItem(id: number | undefined) {
		if (window.confirm("Are you sure?") && id !== undefined) {
			try {
				await SensorsService.prototype.delete(id);
				document.location.reload();
			} catch (err: any) {
				// errors that expected from back
				alert(err);
			}
		}
	}

	const getItems = async (): Promise<void> => {
		try {
			const response: Sensor[] | null = await SensorsService.prototype.getAll();
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
			{/* // TODO Language */}
			<div className="d-flex border border-dark w-100">
				<AdminPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>Sensors</h1>
					</div>
				</header>
				<div className="container mt-5">
					<Button onClick={() => addModalShow()} variant="outline-primary">
						Create
					</Button>
				</div>
				<Modal size="lg" centered show={createItemModelShow} onHide={createItemModelHandleClose}>
					<CreateSensor close={createItemModelHandleClose} />
				</Modal>

				<Modal size="lg" centered show={updateItemModelShow} onHide={updateItemModelHandleClose}>
					<UpdateSensor close={updateItemModelHandleClose} item={item} />
				</Modal>
				<main>
					<div className="mt-5">
						<Table className="table table-striped auto__table text-center" striped bordered hover size="lg">
							<thead>
								<tr>
									<th>Id</th>
									<th>Smart device id</th>
									<th></th>
								</tr>
							</thead>
							<tbody>
								{items ? (
									items.map(e => (
										<tr key={e.sensorId}>
											<td>{e.sensorId}</td>
											<td>{e.smartDeviceId}</td>
											<td>
												<Button onClick={() => handleEditItem(e.sensorId)} variant="outline-dark">
													Update
												</Button>
												<Button onClick={() => handleDeleteItem(e.sensorId)} variant="outline-danger">
													Delete
												</Button>
											</td>
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
