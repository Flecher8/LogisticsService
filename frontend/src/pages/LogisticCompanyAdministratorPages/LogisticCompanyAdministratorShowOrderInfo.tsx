import React, { useState, useEffect, FC } from "react";
import { useParams } from "react-router-dom";
import { Button, Card, Modal } from "react-bootstrap";
import { Order, OrderStatus, OrdersService } from "../../api/services/OrdersService";
import { OrderInfoCard } from "../../components/OrderInfoCard";
import { CancelOrder } from "../../components/CancelOrder";
import { LogisticCompaniesAdministratorPanel } from "../../components/LogisticCompaniesAdministratorPanel";

export const LogisticCompanyAdministratorShowOrderInfo: FC = () => {
	const [order, setOrder] = useState<Order | null>(null);
	const { id } = useParams<{ id: string }>();

	// Cancel modal show
	const [cancelOrderModelShow, SetCancelOrderModelShow] = useState(false);
	const cancelOrderModelHandleClose = () => SetCancelOrderModelShow(false);
	const cancelOrderModelHandleShow = () => SetCancelOrderModelShow(true);

	const getOrder = async (): Promise<void> => {
		try {
			if (id === undefined) return;

			const response: Order | null = await OrdersService.prototype.getOrder(parseInt(id));
			setOrder(response);
			if (response == null) return;
		} catch (err) {}
	};

	function handleCancelOrder() {
		cancelOrderModelHandleShow();
	}

	useEffect(() => {
		getOrder();
	}, []);
	return (
		<div className="LogisticCompanyAdministratorShowOrderInfo container">
			{/* // TODO Language */}
			<div className="d-flex border border-dark w-100">
				<LogisticCompaniesAdministratorPanel />
			</div>

			<Modal size="lg" centered show={cancelOrderModelShow} onHide={cancelOrderModelHandleClose}>
				<CancelOrder close={cancelOrderModelHandleClose} orderId={order === null ? 0 : order.orderId} />
			</Modal>
			{order !== null && order.orderStatus === OrderStatus.WaitingForAcceptanceByLogisticCompany ? (
				<div className="mt-5">
					<div>
						<h4>Order management</h4>
					</div>
					<div>
						<Button onClick={() => handleCancelOrder()} variant="outline-danger">
							Cancel order
						</Button>
						{/*//TODO Update order, write driver id and sensor id */}
					</div>
				</div>
			) : null}
			{order !== null && order.orderStatus === OrderStatus.WaitingForPaymentByPrivateCompany ? (
				<div className="m-5">
					<div>
						<h4>Order management</h4>
					</div>
					<div className="d-flex flex-row">
						<div className="mr-3">
							<Button onClick={() => handleCancelOrder()} variant="outline-danger">
								Cancel order
							</Button>
						</div>
					</div>
				</div>
			) : null}
			<div>{order !== null ? <OrderInfoCard order={order} /> : <p>No data</p>}</div>
		</div>
	);
};
