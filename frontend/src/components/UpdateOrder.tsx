import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import { OrderDto, OrdersService } from "../api/services/OrdersService";

interface CancelOrderProps {
	orderId: number;
	close(): void;
}

export const UpdateOrder: FC<CancelOrderProps> = ({ orderId, close }) => {
	const [driverId, setDriverId] = useState<number>(0);
	const [sensorId, setSensorId] = useState<number>(0);

	async function handle() {
		try {
			let order: OrderDto = {
				orderId: orderId,
				privateCompanyId: 0,
				logisticCompanyId: 0,
				logisticCompaniesDriverId: driverId,
				sensorId: sensorId,
				cargoId: 0,
				startDeliveryAddressId: 0,
				endDeliveryAddressId: 0,
				estimatedDeliveryDateTime: "2023-05-31T09:09:00.0000000"
			};
			await OrdersService.prototype.update(order);
			close();
			window.location.reload();
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
		<div className="UpdateOrder">
			<div className="container">
				<header>
					<div className="text-center mt-5">
						<h1>Update order</h1>
					</div>
				</header>
				<div>
					<div className="mt-5 ml-5 mr-5">
						<Form>
							<Form.Group className="mb-3">
								<Form.Label>Driver Id</Form.Label>
								<Form.Control
									type="number"
									placeholder=""
									value={driverId}
									min={0}
									onChange={e => setDriverId(parseInt(e.target.value))}
								/>
							</Form.Group>

							<Form.Group className="mb-3">
								<Form.Label>Description</Form.Label>
								<Form.Control
									type="number"
									placeholder=""
									value={sensorId}
									min={0}
									onChange={e => setSensorId(parseInt(e.target.value))}
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
