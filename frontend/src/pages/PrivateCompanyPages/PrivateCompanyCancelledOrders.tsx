import { useState, useEffect, FC } from "react";
import { Button, Card } from "react-bootstrap";
import { Link } from "react-router-dom";
import { PrivateCompanyPanel } from "../../components/PrivateCompanyPanel";
import { Order, OrdersService } from "../../api/services/OrdersService";
import { OrderCard } from "../../components/OrderCard";

export const PrivateCompanyCancelledOrders: FC = () => {
	const [orders, setOrders] = useState<Order[] | null>();

	const getOrders = async (): Promise<void> => {
		try {
			const userId: number = localStorage["userId"];
			const response: Order[] | null = await OrdersService.prototype.getCancelledOrders(userId);
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
		<div className="PrivateCompanyCancelledOrders container">
			{/* // TODO Language */}
			<div className="d-flex border border-dark w-100">
				<PrivateCompanyPanel />
			</div>
			<div className="d-flex flex-column">
				<header>
					<div className="text-center mt-5">
						<h1>Cancelled orders</h1>
					</div>
				</header>
				<div className="d-flex flex-column align-items-start flex-fill flex-wrap">
					{orders ? (
						orders.map(order => (
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
