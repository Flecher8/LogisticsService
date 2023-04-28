import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import { Link } from "react-router-dom";
import { AdminPanel } from "../../components/AdminPanel";
import { SubscriptionType, SubscriptionTypesService } from "../../api/services/SubscriptionTypesService";

export const AdminSubscriptionTypes: FC = () => {
	const [subscriptionTypes, setSubscriptionTypes] = useState<SubscriptionType[] | null>([]);

	const getSubscriptionTypesValues = async (): Promise<void> => {
		try {
			const response: SubscriptionType[] | null = await SubscriptionTypesService.prototype.getSubscriptionTypes();
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
					{/* <Button onClick={() => addModalShow()} variant="outline-primary">
						Створити
					</Button> */}
					123
				</div>
				{/* <Modal size="lg" centered show={createItemModelShow} onHide={createItemModelHandleClose}>
					<CreateGameComponent close={createItemModelHandleClose} />
				</Modal> */}

				{/* <Modal size="lg" centered show={updateItemModelShow} onHide={updateItemModelHandleClose}>
					<UpdateGameComponent close={updateItemModelHandleClose} game={game} />
				</Modal> */}
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
									(console.log(subscriptionTypes),
									subscriptionTypes.map(e => (
										<tr key={e.subscriptionTypeId}>
											<td>{e.subscriptionTypeName}</td>
											<td>{e.durationInDays}</td>
											<td>{e.price}</td>
											<td>
												{/* <Button onClick={() => handleEditGame(e._id)} variant="outline-dark">
													Змінити
												</Button>
												<Button onClick={() => handleDeleteGame(e._id)} variant="outline-danger">
													Видалити
												</Button> */}
											</td>
										</tr>
									)))
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
