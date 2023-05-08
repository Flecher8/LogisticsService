import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import {
	LogisticCompaniesAdministratorDto,
	LogisticCompaniesAdministratorsService
} from "../../api/services/LogisticCompaniesAdministratorsService";
import { LogisticCompanyPanel } from "../../components/LogisticCompanyPanel";
import { CreateLogisticCompaniesAdministrators } from "../../components/CreateLogisticCompaniesAdministrators";
import { useTranslationHelper } from "../../helpers/translation/translationService";

export const LogisticCompanyChangeAdministrators: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	const [items, setItems] = useState<LogisticCompaniesAdministratorDto[] | null>([]);

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
			const response: LogisticCompaniesAdministratorDto[] | null =
				await LogisticCompaniesAdministratorsService.prototype.getItemsByLogisticCompany(userId);
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
			<div className="d-flex border border-dark w-100">
				<LogisticCompanyPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>{t("Administrators")}</h1>
					</div>
				</header>
				<div className="container mt-5">
					<Button onClick={() => addModalShow()} variant="outline-primary">
						{t("Create")}
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
									<th>{t("First name")}</th>
									<th>{t("Last name")}</th>
									<th>{t("Email")}</th>
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
