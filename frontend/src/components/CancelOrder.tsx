import { useState, useEffect, FC } from "react";
import { Link } from "react-router-dom";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import { CancelledOrder, CancelledOrderService } from "../api/services/CancelledOrderService";

interface CancelOrderProps {
	orderId: number;
	close(): void;
}

export const CancelOrder: FC<CancelOrderProps> = ({ orderId, close }) => {
	const [reason, setReason] = useState<string>("");
	const [description, setDescription] = useState<string>("");

	async function handle() {
		try {
			let cancelledOrder: CancelledOrder = {
				orderId: orderId,
				reason: reason,
				description: description,
				cancelledBy: localStorage["userType"],
				cancelledById: parseInt(localStorage["userId"])
			};
			await CancelledOrderService.prototype.cancelOrder(cancelledOrder);
			close();
		} catch (err) {
			alert(err);
		}
	}
	useEffect(() => {
		if (orderId === 0) {
			close();
		}
	}, []);

	return (
		<div className="CreateSubscriptionType">
			<div className="container">
				<header>
					<div className="text-center mt-5">
						<h1>Cancel order</h1>
					</div>
				</header>
				<div>
					<div className="mt-5 ml-5 mr-5">
						<Form>
							<Form.Group className="mb-3">
								<Form.Label>Reason</Form.Label>
								<Form.Control
									type="text"
									placeholder=""
									value={reason}
									onChange={e => setReason(e.target.value)}
								/>
							</Form.Group>

							<Form.Group className="mb-3">
								<Form.Label>Description</Form.Label>
								<Form.Control
									type="text"
									placeholder=""
									value={description}
									onChange={e => setDescription(e.target.value)}
								/>
							</Form.Group>
						</Form>
						<div>
							<Button className="mb-5" onClick={() => handle()}>
								OK
							</Button>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};
