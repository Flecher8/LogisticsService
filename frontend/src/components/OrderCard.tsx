import { useState, useEffect, FC } from "react";
import { Card } from "react-bootstrap";
import { Order, OrderStatus } from "../api/services/OrdersService";
import { DataTimeService } from "../api/services/DataTimeService";

interface OrderCardProps {
	order: Order;
}

export const OrderCard: FC<OrderCardProps> = ({ order }) => {
	const getOrderStatus = (status: number): string => {
		const orderStatus = OrderStatus[status];
		if (orderStatus !== undefined) {
			return orderStatus.substring(0, 1) + orderStatus.substring(1).toLowerCase();
		} else {
			return "Unknown order status";
		}
	};
	return (
		<div className="OrderCard container">
			<div>
				<Card
					className="my-5"
					style={{ width: "18rem", boxShadow: "0px 2px 4px rgba(0, 0, 0, 0.25)", cursor: "pointer" }}>
					<Card.Header>Order № {`${order.orderId}`}</Card.Header>
					<Card.Body>
						<Card.Text>
							Creation date and time:{" "}
							{`${DataTimeService.prototype.getLocalDataByUTCData(order.creationDateTime)}`}
						</Card.Text>
						<Card.Text>Order status: {`${getOrderStatus(order.orderStatus)}`}</Card.Text>
					</Card.Body>
				</Card>
			</div>
		</div>
	);
};
