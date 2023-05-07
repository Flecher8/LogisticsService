import { useState, useEffect, FC } from "react";
import { Card } from "react-bootstrap";
import { Order, OrderStatus, OrdersService, Point } from "../api/services/OrdersService";
import { DataTimeService } from "../helpers/DataTimeService";
import { CargoCard } from "./CargoCard";
import { GoogleMaps } from "./GoogleMap";
import { GeolocationService } from "../api/services/GeolocationService";
import { Address } from "../api/services/AddressesService";

interface OrderCardProps {
	order: Order;
}

export const OrderInfoCard: FC<OrderCardProps> = ({ order }) => {
	const [loaded, setLoaded] = useState<boolean>(false);
	const [point, setPoint] = useState<Point>();
	const [showMap, setShowMap] = useState<boolean>(false);

	const [startDeliveryAddress, setStartDeliveryAddress] = useState<string>("");
	const [endDeliveryAddress, setEndDeliveryAddress] = useState<string>("");

	const loadData = async (order: Order): Promise<void> => {
		if (order?.orderStatus === OrderStatus.InTransit) {
			await loadMap();
		}
		// TODO SHOW Address
		//await loadAddresses(order);

		setLoaded(true);
	};

	const loadMap = async (): Promise<void> => {
		await getCurrentLocation();
		setShowMap(true);
	};

	const loadAddresses = async (order: Order): Promise<void> => {
		let startAddress: string | undefined = await getAddress(order.startDeliveryAddress);
		let endAddress: string | undefined = await getAddress(order.endDeliveryAddress);
		if (startAddress === undefined || endAddress === undefined) return;
		setStartDeliveryAddress(startAddress);
		setEndDeliveryAddress(endAddress);
	};

	const getCurrentLocation = async (): Promise<void> => {
		try {
			if (order.orderId === undefined) return;

			const response: Point | null = await OrdersService.prototype.getOrderCurrentLocation(order.orderId);
			if (response == null) return;
			setPoint(response);
		} catch (err) {}
	};

	const getAddress = async (address: Address): Promise<string | undefined> => {
		try {
			const response: string | null = await GeolocationService.prototype.getAddress(
				address,
				localStorage["i18nextLng"]
			);

			return response !== null ? response : "";
		} catch (err) {}
	};

	const getLocalDataByUTCData = (dataTime: string | undefined): string => {
		if (dataTime === undefined) return "";
		return DataTimeService.prototype.getLocalDataByUTCData(dataTime);
	};

	useEffect(() => {
		if (!loaded) {
			loadData(order);
		}
	}, []);
	return (
		<div className="PrivateCompanyShowOrderInfo container">
			{/* // TODO Language */}
			<Card>
				<Card.Header>
					<h1 className="text-center">Order Info</h1>
				</Card.Header>
				{order ? (
					<Card.Body>
						<Card.Title>Order Id: {order.orderId}</Card.Title>
						<Card.Text>Private company name: {order.privateCompany.companyName}</Card.Text>
						<Card.Text>Logistic company name: {order.logisticCompany.companyName}</Card.Text>
						<Card.Text>Start delivery address: {startDeliveryAddress}</Card.Text>
						<Card.Text>End delivery address: {endDeliveryAddress}</Card.Text>
						<Card.Text>Creation data and time: {getLocalDataByUTCData(order.creationDateTime)}</Card.Text>
						<Card.Text>
							Estimated delivery date and time: {getLocalDataByUTCData(order.estimatedDeliveryDateTime)}
						</Card.Text>
						<Card.Text>Price: {order.price} $</Card.Text>
						{order.logisticCompaniesDriver !== null && order.logisticCompaniesDriver !== undefined ? (
							<Card.Text>{`Driver name: ${order.logisticCompaniesDriver?.firstName} + " " + ${order.logisticCompaniesDriver?.lastName}`}</Card.Text>
						) : null}
						{localStorage["userType"] === "LogisticCompanyAdministrator" &&
						order.sensor !== null &&
						order.sensor !== undefined ? (
							<Card.Text>Sensor id: {`${order.sensor?.sensorId}`}</Card.Text>
						) : null}
						<Card.Text>{order.cargo ? <CargoCard cargo={order.cargo} /> : <p>No data</p>}</Card.Text>
						<Card.Text>
							{/* //TODO SHOW MAP  */}
							{/* {showMap && point ? (
								<div>
									<p>Current location:</p>
									<GoogleMaps lat={point.latitude} lng={point.longitude} />
								</div>
							) : null} */}
						</Card.Text>
					</Card.Body>
				) : (
					<Card.Body>
						<Card.Title>No data</Card.Title>
					</Card.Body>
				)}
			</Card>
		</div>
	);
};
