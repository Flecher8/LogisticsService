import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import { Link } from "react-router-dom";
import { AdminPanel } from "../../components/AdminPanel";
import { SubscriptionType, SubscriptionTypesService } from "../../api/services/SubscriptionTypesService";
import { UpdateSubscriptionType } from "../../components/AdminSubscriptionTypeComponents/UpdateSubscriptionType";
import { CreateSubscriptionType } from "../../components/AdminSubscriptionTypeComponents/CreateSubscriptionType";
import { useTranslationHelper } from "../../helpers/translation/translationService";

export const AdminSubscriptionTypes: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	const [items, setItems] = useState<SubscriptionType[] | null>([]);
	const [item, setItem] = useState<SubscriptionType | undefined>();

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
		setItem(items?.find(e => e.subscriptionTypeId === id));
		updateItemModelHandleShow();
	}

	async function handleDeleteItem(id: number | undefined) {
		const confirmationText = t("Are you sure?") ?? "";
		if (window.confirm(confirmationText) && id !== undefined) {
			try {
				await SubscriptionTypesService.prototype.delete(id);
				document.location.reload();
			} catch (err: any) {
				// errors that expected from back
				alert(err);
			}
		}
	}

	const getSubscriptionTypesValues = async (): Promise<void> => {
		try {
			const response: SubscriptionType[] | null = await SubscriptionTypesService.prototype.getAll();
			if (response === null) {
			}
			setItems(response);
		} catch (err) {
			alert(err);
		}
	};

	useEffect(() => {
		getSubscriptionTypesValues();
	}, []);
	return (
		<div className="AdminSubscriptionTypes container">
			<div className="d-flex border border-dark w-100">
				<AdminPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>{t("Subscription Types")}</h1>
					</div>
				</header>
				<div className="container mt-5">
					<Button onClick={() => addModalShow()} variant="outline-primary">
						{t("Create")}
					</Button>
				</div>
				<Modal size="lg" centered show={createItemModelShow} onHide={createItemModelHandleClose}>
					<CreateSubscriptionType close={createItemModelHandleClose} />
				</Modal>

				<Modal size="lg" centered show={updateItemModelShow} onHide={updateItemModelHandleClose}>
					<UpdateSubscriptionType close={updateItemModelHandleClose} item={item} />
				</Modal>
				<main>
					<div className="mt-5">
						<Table className="table table-striped auto__table text-center" striped bordered hover size="lg">
							<thead>
								<tr>
									<th>{t("Subscription Type Name")}</th>
									<th>{t("Duration in days")}</th>
									<th>{t("Price")}</th>
									<th></th>
								</tr>
							</thead>
							<tbody>
								{items ? (
									items.map(e => (
										<tr key={e.subscriptionTypeId}>
											<td>{e.subscriptionTypeName}</td>
											<td>{e.durationInDays}</td>
											<td>{e.price}</td>
											<td>
												<Button onClick={() => handleEditItem(e.subscriptionTypeId)} variant="outline-dark">
													{t("Update")}
												</Button>
												<Button
													onClick={() => handleDeleteItem(e.subscriptionTypeId)}
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
