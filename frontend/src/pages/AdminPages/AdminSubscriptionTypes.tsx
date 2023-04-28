import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import { Link } from "react-router-dom";
import { AdminPanel } from "../../components/AdminPanel";
import { SubscriptionType, SubscriptionTypesService } from "../../api/services/SubscriptionTypesService";
import { UpdateSubscriptionType } from "../../components/AdminSubscriptionTypeComponents/UpdateSubscriptionType";
import { CreateSubscriptionType } from "../../components/AdminSubscriptionTypeComponents/CreateSubscriptionType";
// TODO Language
export const AdminSubscriptionTypes: FC = () => {
	const [subscriptionTypes, setSubscriptionTypes] = useState<SubscriptionType[] | null>([]);
	const [subscriptionType, setSubscriptionType] = useState<SubscriptionType | undefined>();

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
		setSubscriptionType(subscriptionTypes?.find(e => e.subscriptionTypeId === id));
		updateItemModelHandleShow();
	}

	async function handleDeleteItem(id: number | undefined) {
		if (window.confirm("Are you sure?") && id !== undefined) {
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
			setSubscriptionTypes(response);
		} catch (err) {
			alert(err);
		}
	};

	useEffect(() => {
		getSubscriptionTypesValues();
	}, []);
	return (
		<div className="AdminSubscriptionTypes container">
			{/* // TODO Language */}
			<div className="d-flex border border-dark w-100">
				<AdminPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>Subscription Types</h1>
					</div>
				</header>
				<div className="container mt-5">
					<Button onClick={() => addModalShow()} variant="outline-primary">
						Create
					</Button>
				</div>
				<Modal size="lg" centered show={createItemModelShow} onHide={createItemModelHandleClose}>
					<CreateSubscriptionType close={createItemModelHandleClose} />
				</Modal>

				<Modal size="lg" centered show={updateItemModelShow} onHide={updateItemModelHandleClose}>
					<UpdateSubscriptionType close={updateItemModelHandleClose} item={subscriptionType} />
				</Modal>
				<main>
					<div className="mt-5">
						<Table className="table table-striped auto__table text-center" striped bordered hover size="lg">
							<thead>
								<tr>
									<th>subscriptionTypeName</th>
									<th>durationInDays</th>
									<th>price</th>
									<th></th>
								</tr>
							</thead>
							<tbody>
								{subscriptionTypes ? (
									subscriptionTypes.map(e => (
										<tr key={e.subscriptionTypeId}>
											<td>{e.subscriptionTypeName}</td>
											<td>{e.durationInDays}</td>
											<td>{e.price}</td>
											<td>
												<Button onClick={() => handleEditItem(e.subscriptionTypeId)} variant="outline-dark">
													Update
												</Button>
												<Button
													onClick={() => handleDeleteItem(e.subscriptionTypeId)}
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
