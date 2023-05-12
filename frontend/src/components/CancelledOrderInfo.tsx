import { useState, useEffect, FC } from "react";
import { Card } from "react-bootstrap";
import { Order, OrderStatus, OrdersService, Point } from "../api/services/OrdersService";
import { DataTimeService } from "../helpers/DataTimeService";
import { CargoCard } from "./CargoCard";
import { GoogleMaps } from "./GoogleMap";
import { GeolocationService } from "../api/services/GeolocationService";
import { Address } from "../api/services/AddressesService";
import { useTranslationHelper } from "../helpers/translation/translationService";
import { CancelledOrder, CancelledOrderService } from "../api/services/CancelledOrderService";

interface CancelledOrderInfoProps {
	orderId: number;
}

export const CancelledOrderInfo: FC<CancelledOrderInfoProps> = ({ orderId }) => {
	const { t, changeLanguage } = useTranslationHelper();

	const [cancelledOrder, setCancelledOrder] = useState<CancelledOrder | null>(null);

	const loadData = async (): Promise<void> => {
		try {
			const response: CancelledOrder | null = await CancelledOrderService.prototype.cancelledOrderByOrderId(orderId);
			setCancelledOrder(response);
		} catch (err: any) {}
	};

	useEffect(() => {
		loadData();
		console.log(123);
	}, []);
	return (
		<div className="CancelledOrderInfo container">
			<Card>
				<Card.Header>
					<h1 className="text-center">{t("Cancelled Order Info")}</h1>
				</Card.Header>
				{cancelledOrder ? (
					<Card.Body>
						<Card.Text>
							{t("Reason")}: {cancelledOrder.reason}
						</Card.Text>
						<Card.Text>
							{t("Description")}: {cancelledOrder.description}
						</Card.Text>
					</Card.Body>
				) : (
					<Card.Body>
						<Card.Title>{t("No data")}</Card.Title>
					</Card.Body>
				)}
			</Card>
		</div>
	);
};
