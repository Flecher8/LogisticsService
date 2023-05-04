import React, { useState, useEffect, FC } from "react";
import { useParams } from "react-router-dom";
import { Button, Card } from "react-bootstrap";
import { PrivateCompanyPanel } from "../../components/PrivateCompanyPanel";
import { ActiveOrders, Address, Order, OrderStatus, OrdersService, Point } from "../../api/services/OrdersService";
import { OrderCard } from "../../components/OrderCard";
import { GoogleMaps } from "../../components/GoogleMap";
import { GeolocationService } from "../../api/services/GeolocationService";
import { DataTimeService } from "../../helpers/DataTimeService";
import { CargoCard } from "../../components/CargoCard";

export const PrivateCompanyShowOrderInfo: FC = () => {
	const [order, setOrder] = useState<Order | null>(null);
	const { id } = useParams<{ id: string }>();

	const getOrder = async (): Promise<void> => {
		try {
			if (id === undefined) return;

			const response: Order | null = await OrdersService.prototype.getOrder(parseInt(id));
			setOrder(response);
			if (response == null) return;
		} catch (err) {}
	};

	useEffect(() => {
		getOrder();
	}, []);
	return (
		<div className="PrivateCompanyShowOrderInfo container">
			{/* // TODO Language */}
			<div className="d-flex border border-dark w-100">
				<PrivateCompanyPanel />
			</div>
			<div>{order !== null ? <OrderCard order={order} /> : <p>No data</p>}</div>
		</div>
	);
};
