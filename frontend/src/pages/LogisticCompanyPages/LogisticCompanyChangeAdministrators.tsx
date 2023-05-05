import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import { AdminPanel } from "../../components/AdminPanel";
import { SmartDevice, SmartDevicesService } from "../../api/services/SmartDevicesService";
import {
	LogisticCompaniesAdministrator,
	LogisticCompaniesAdministratorsService
} from "../../api/services/LogisticCompaniesAdministratorsService";
import { LogisticCompanyPanel } from "../../components/LogisticCompanyPanel";
import { CreateLogisticCompaniesAdministrators } from "../../components/CreateLogisticCompaniesAdministrators";

// TODO Language
export const LogisticCompanyChangeAdministrators: FC = () => {
	const [items, setItems] = useState<LogisticCompaniesAdministrator[] | null>([]);

	// Create modal show
	const [createItemModelShow, SetCreateItemModelShow] = useState(false);
	const createItemModelHandleClose = () => SetCreateItemModelShow(false);
	const createItemModelHandleShow = () => SetCreateItemModelShow(true);

	// Show add modal function
	function addModalShow() {
		createItemModelHandleShow();
	}

	async function handleDeleteItem(id: number | undefined) {
		if (id !== undefined && window.confirm("Are you sure?")) {
			try {
				await LogisticCompaniesAdministratorsService.prototype.delete(id);
				window.location.reload();
			} catch (err: any) {
				// errors that expected from back
				alert(err);
			}
		}
	}

	const getItems = async (): Promise<void> => {
		try {
			const userId: number = parseInt(localStorage["userId"]);
			const response: LogisticCompaniesAdministrator[] | null =
				await LogisticCompaniesAdministratorsService.prototype.getItemByLogisticCompany(userId);
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
		<div className="LogisticCompanyChangeAdministrators container">
			{/* // TODO Language */}
			<div className="d-flex border border-dark w-100">
				<LogisticCompanyPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>Administrators</h1>
					</div>
				</header>
				<div className="container mt-5">
					<Button onClick={() => addModalShow()} variant="outline-primary">
						Create
					</Button>
				</div>
				<Modal size="lg" centered show={createItemModelShow} onHide={createItemModelHandleClose}>
					<CreateLogisticCompaniesAdministrators close={createItemModelHandleClose} />
				</Modal>

				<main>
					<div className="mt-5">
						<Table className="table table-striped auto__table text-center" striped bordered hover size="lg">
							<thead>
								<tr>
									<th>First name</th>
									<th>Last name</th>
									<th>Email</th>
									<th></th>
								</tr>
							</thead>
							<tbody>
								{items ? (
									items.map(e => (
										<tr key={e.logisticCompaniesAdministratorId}>
											<td>{e.firstName}</td>
											<td>{e.lastName}</td>
											<td>{e.email}</td>
											<td>
												<Button
													onClick={() => handleDeleteItem(e.logisticCompaniesAdministratorId)}
													variant="outline-danger">
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
