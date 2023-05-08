import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import {
	LogisticCompaniesDriver,
	LogisticCompaniesDriversService
} from "../../api/services/LogisticCompaniesDriversService";
import { LogisticCompaniesAdministratorPanel } from "../../components/LogisticCompaniesAdministratorPanel";
import { LogisticCompaniesAdministrator } from "../../api/services/LogisticCompaniesAdministratorsService";
import { LogisticCompaniesAdministratorsService } from "../../api/services/LogisticCompaniesAdministratorsService";
import { CreateLogisticCompaniesDriver } from "../../components/CreateLogisticCompaniesDriver";
import { useTranslationHelper } from "../../helpers/translation/translationService";

export const LogisticCompanyAdministratorChangeDrivers: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	const [items, setItems] = useState<LogisticCompaniesDriver[] | null>([]);

	// Create modal show
	const [createItemModelShow, SetCreateItemModelShow] = useState(false);
	const createItemModelHandleClose = () => SetCreateItemModelShow(false);
	const createItemModelHandleShow = () => SetCreateItemModelShow(true);

	// Show add modal function
	function addModalShow() {
		createItemModelHandleShow();
	}

	async function handleDeleteItem(id: number | undefined) {
		const confirmationText = t("Are you sure?") ?? "";
		if (id !== undefined && window.confirm(confirmationText)) {
			try {
				await LogisticCompaniesDriversService.prototype.delete(id);
				window.location.reload();
			} catch (err: any) {
				// errors that expected from back
				alert(err);
			}
		}
	}

	const getItems = async (): Promise<void> => {
		try {
			let logisticCompanyId: number = await getLogisticCompanyId();

			const response: LogisticCompaniesDriver[] | null =
				await LogisticCompaniesDriversService.prototype.getItemsByLogisticCompany(logisticCompanyId);

			if (response === null) return;

			setItems(response);
		} catch (err) {
			alert(err);
		}
	};

	async function getLogisticCompanyId(): Promise<number> {
		const userId: number = parseInt(localStorage["userId"]);

		const admin: LogisticCompaniesAdministrator | null =
			await LogisticCompaniesAdministratorsService.prototype.getItem(userId);

		if (admin === null) return 0;

		let logisticCompanyId: number = admin.logisticCompany.logisticCompanyId;
		return logisticCompanyId;
	}

	useEffect(() => {
		getItems();
	}, []);
	return (
		<div className="LogisticCompanyAdministratorChangeDrivers container">
			<div className="d-flex border border-dark w-100">
				<LogisticCompaniesAdministratorPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>{t("Drivers")}</h1>
					</div>
				</header>
				<div className="container mt-5">
					<Button onClick={() => addModalShow()} variant="outline-primary">
						{t("Create")}
					</Button>
				</div>
				<Modal size="lg" centered show={createItemModelShow} onHide={createItemModelHandleClose}>
					<CreateLogisticCompaniesDriver close={createItemModelHandleClose} />
				</Modal>

				<main>
					<div className="mt-5">
						<Table className="table table-striped auto__table text-center" striped bordered hover size="lg">
							<thead>
								<tr>
									<th>{t("Id")}</th>
									<th>{t("First name")}</th>
									<th>{t("Last name")}</th>
									<th>{t("Email")}</th>
									<th></th>
								</tr>
							</thead>
							<tbody>
								{items ? (
									items.map(e => (
										<tr key={e.logisticCompaniesDriverId}>
											<td>{e.logisticCompaniesDriverId}</td>
											<td>{e.firstName}</td>
											<td>{e.lastName}</td>
											<td>{e.email}</td>
											<td>
												<Button
													onClick={() => handleDeleteItem(e.logisticCompaniesDriverId)}
													variant="outline-danger">
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
