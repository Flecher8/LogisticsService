import React, { useState, useEffect, FC } from "react";
import { useParams } from "react-router-dom";
import { Button, Card, Modal } from "react-bootstrap";
import { PrivateCompanyPanel } from "../../components/PrivateCompanyPanel";
import { Order, OrderStatus, OrdersService } from "../../api/services/OrdersService";
import { OrderInfoCard } from "../../components/OrderInfoCard";
import { CancelOrder } from "../../components/CancelOrder";
import { PaymentOrder } from "../../components/PaymentOrder";
import { useTranslationHelper } from "../../helpers/translation/translationService";

export const PrivateCompanyShowOrderInfo: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	const [order, setOrder] = useState<Order | null>(null);
	const { id } = useParams<{ id: string }>();

	// Cancel modal show
	const [cancelOrderModelShow, SetCancelOrderModelShow] = useState(false);
	const cancelOrderModelHandleClose = () => SetCancelOrderModelShow(false);
	const cancelOrderModelHandleShow = () => SetCancelOrderModelShow(true);

	// Payment modal show
	const [paymentOrderModelShow, SetPaymentOrderModelShow] = useState(false);
	const paymentOrderModelHandleClose = () => SetPaymentOrderModelShow(false);
	const paymentOrderModelHandleShow = () => SetPaymentOrderModelShow(true);

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

	function handlePaymentOrder() {
		paymentOrderModelHandleShow();
	}

	useEffect(() => {
		getOrder();
	}, []);
	return (
		<div className="PrivateCompanyShowOrderInfo container">
			<div className="d-flex border border-dark w-100">
				<PrivateCompanyPanel />
			</div>

			<Modal size="lg" centered show={cancelOrderModelShow} onHide={cancelOrderModelHandleClose}>
				<CancelOrder close={cancelOrderModelHandleClose} orderId={order === null ? 0 : order.orderId} />
			</Modal>

			<Modal size="lg" centered show={paymentOrderModelShow} onHide={paymentOrderModelHandleClose}>
				<PaymentOrder
					close={paymentOrderModelHandleClose}
					orderId={order === null ? 0 : order.orderId}
					price={order === null ? 0 : order.price}
				/>
			</Modal>
			{order !== null && order.orderStatus === OrderStatus.WaitingForAcceptanceByLogisticCompany ? (
				<div className="mt-5">
					<div>
						<h4>{t("Order management")}</h4>
					</div>
					<div>
						<Button onClick={() => handleCancelOrder()} variant="outline-danger">
							{t("Cancel order")}
						</Button>
					</div>
				</div>
			) : null}
			{order !== null && order.orderStatus === OrderStatus.WaitingForPaymentByPrivateCompany ? (
				<div className="m-5">
					<div>
						<h4>{t("Order management")}</h4>
					</div>
					<div className="d-flex flex-row">
						<div className="mr-3 ">
							<Button onClick={() => handlePaymentOrder()} variant="outline-primary">
								{t("Payment")}
							</Button>
						</div>
						<div className="mr-3">
							<Button onClick={() => handleCancelOrder()} variant="outline-danger">
								{t("Cancel order")}
							</Button>
						</div>
					</div>
				</div>
			) : null}
			<div>{order !== null ? <OrderInfoCard order={order} /> : <p>{t("No data")}</p>}</div>
		</div>
	);
};
