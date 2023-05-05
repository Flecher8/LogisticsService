import { useState, useEffect, FC } from "react";
import { Card } from "react-bootstrap";
import { Order, OrderStatus } from "../api/services/OrdersService";
import { DataTimeService } from "../helpers/DataTimeService";

interface OrderCardProps {
	order: Order;
}

export const OrderCard: FC<OrderCardProps> = ({ order }) => {
	return (
		<div className="OrderCard container">
			<Card
				className="my-5"
				style={{ width: "18rem", boxShadow: "0px 2px 4px rgba(0, 0, 0, 0.25)", cursor: "pointer" }}>
				<Card.Header>Order № {`${order.orderId}`}</Card.Header>
				<Card.Body>
					<Card.Text>
						Creation date and time: {`${DataTimeService.prototype.getLocalDataByUTCData(order.creationDateTime)}`}
					</Card.Text>
				</Card.Body>
			</Card>
		</div>
	);
};
