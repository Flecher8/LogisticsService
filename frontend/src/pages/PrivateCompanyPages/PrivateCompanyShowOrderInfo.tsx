import React, { useState, useEffect, FC } from "react";
import { useParams } from "react-router-dom";
import { Button, Card } from "react-bootstrap";
import { PrivateCompanyPanel } from "../../components/PrivateCompanyPanel";
import { ActiveOrders, Order, OrdersService } from "../../api/services/OrdersService";
import { OrderCard } from "../../components/OrderCard";
import { GoogleMaps } from "../../components/GoogleMap";

export const PrivateCompanyShowOrderInfo: FC = () => {
	const [order, setOrder] = useState<Order | null>();
	const { id } = useParams<{ id: string }>();

	const getOrder = async (): Promise<void> => {
		try {
			if (id === undefined) return;

			const response: Order | null = await OrdersService.prototype.getOrder(parseInt(id));
			setOrder(response);
			console.log(response);
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
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>Order Info</h1>
					</div>
				</header>
				<div>
					<GoogleMaps lat={50.84281197725498} lng={4.3823823826825405} />
				</div>
			</div>
		</div>
	);
};
