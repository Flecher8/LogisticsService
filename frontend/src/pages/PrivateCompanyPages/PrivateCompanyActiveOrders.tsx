import { useState, useEffect, FC } from "react";
import { Button, Card } from "react-bootstrap";
import { Link } from "react-router-dom";
import { PrivateCompanyPanel } from "../../components/PrivateCompanyPanel";
import { ActiveOrders, Order, OrdersService } from "../../api/services/OrdersService";
import { OrderCard } from "../../components/OrderCard";

export const PrivateCompanyActiveOrders: FC = () => {
	const [orders, setOrders] = useState<ActiveOrders | null>();

	const getOrders = async (): Promise<void> => {
		try {
			const userId: number = localStorage["userId"];
			const response: ActiveOrders | null = await OrdersService.prototype.getActiveOrders(userId);
			setOrders(response);
		} catch (err) {}
	};

	const handleOrderClick = (orderId: number): void => {
		console.log(orderId);
	};

	useEffect(() => {
		getOrders();
	}, []);
	return (
		<div className="PrivateCompanyActiveOrders container">
			{/* // TODO Language */}
			<div className="d-flex border border-dark w-100">
				<PrivateCompanyPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>Active orders</h1>
					</div>
				</header>
				<div className="mt-5">
					<h2>Waiting for acceptance by logistic company</h2>
					{orders?.waitingForAcceptanceByLogisticCompanyOrders ? (
						orders.waitingForAcceptanceByLogisticCompanyOrders.map(order => (
							<div key={order.orderId} onClick={e => handleOrderClick(order.orderId)}>
								<Link
									key={order.orderId}
									to={`/PrivateCompanyShowOrderInfo/${order.orderId}`}
									style={{ textDecoration: "none", color: "black" }}>
									<OrderCard order={order} />
								</Link>
							</div>
						))
					) : (
						<p>No data</p>
					)}
				</div>
				<div className="mt-5">
					<h2>Waiting for payment</h2>
					{orders?.waitingForPaymentByPrivateCompanyOrders ? (
						orders.waitingForPaymentByPrivateCompanyOrders.map(order => (
							<div key={order.orderId} onClick={e => handleOrderClick(order.orderId)}>
								<OrderCard order={order} />
							</div>
						))
					) : (
						<p>No data</p>
					)}
				</div>
				<div className="mt-5">
					<h2>Accepted orders</h2>
					{orders?.acceptedOrders ? (
						orders.acceptedOrders.map(order => (
							<div key={order.orderId} onClick={e => handleOrderClick(order.orderId)}>
								<OrderCard order={order} />
							</div>
						))
					) : (
						<p>No data</p>
					)}
				</div>
				<div className="mt-5">
					<h2>In transit</h2>
					{orders?.inTransitOrders ? (
						orders.inTransitOrders.map(order => (
							<div key={order.orderId} onClick={e => handleOrderClick(order.orderId)}>
								<OrderCard order={order} />
							</div>
						))
					) : (
						<p>No data</p>
					)}
				</div>
			</div>
		</div>
	);
};
