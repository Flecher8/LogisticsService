import { useState, useEffect, FC } from "react";
import { Button, Card, Form } from "react-bootstrap";
import { PrivateCompanyPanel } from "../../components/PrivateCompanyPanel";
import { PrivateCompanyStatisticsService } from "../../api/services/PrivateCompanyStatisticsService";

interface Option {
	value: string;
	label: string;
}

const options: Option[] = [
	{ value: "km", label: "km" },
	{ value: "mi", label: "mi" }
];

export const PrivateCompanyStatistics: FC = () => {
	const [averageDeliveryTime, setAverageDeliveryTime] = useState<string>("");
	const [averageDeliveryPathLength, setAverageDeliveryPathLength] = useState<number>(0);
	const [numberOfDeliveredOrders, setNumberOfDeliveredOrders] = useState<number>(0);
	const [numberOfNotDeliveredOrders, setNumberOfNotDeliveredOrders] = useState<number>(0);
	const [averageOrderPrice, setAverageOrderPrice] = useState<number>(0);

	const getAverageDeliveryTime = async (userId: number): Promise<void> => {
		try {
			const response: string = await PrivateCompanyStatisticsService.prototype.getAverageDeliveryTime(userId);
			setAverageDeliveryTime(response);
		} catch (err) {}
	};

	const getAverageDeliveryPathLength = async (userId: number, metric: string): Promise<void> => {
		try {
			const response: number = await PrivateCompanyStatisticsService.prototype.getAverageDeliveryPathLength(
				userId,
				metric
			);
			setAverageDeliveryPathLength(response);
		} catch (err) {}
	};

	const getNumberOfDeliveredOrders = async (userId: number): Promise<void> => {
		try {
			const response: number = await PrivateCompanyStatisticsService.prototype.getNumberOfDeliveredOrders(userId);
			setNumberOfDeliveredOrders(response);
		} catch (err) {}
	};

	const getNumberOfNotDeliveredOrders = async (userId: number): Promise<void> => {
		try {
			const response: number = await PrivateCompanyStatisticsService.prototype.getNumberOfNotDeliveredOrders(userId);
			setNumberOfNotDeliveredOrders(response);
		} catch (err) {}
	};

	const getAverageOrderPrice = async (userId: number): Promise<void> => {
		try {
			const response: number = await PrivateCompanyStatisticsService.prototype.getAverageOrderPrice(userId);
			setAverageOrderPrice(response);
		} catch (err) {}
	};

	const getStatistics = async (): Promise<void> => {
		const userId: number = localStorage["userId"];
		await getAverageDeliveryTime(userId);
		await getAverageDeliveryPathLength(userId, options[0].value);
		await getNumberOfDeliveredOrders(userId);
		await getNumberOfNotDeliveredOrders(userId);
		await getAverageOrderPrice(userId);
	};

	const handleSelectChange = async (event: any): Promise<void> => {
		const userId: number = localStorage["userId"];
		await getAverageDeliveryPathLength(userId, event.target.value);
	};

	useEffect(() => {
		getStatistics();
	}, []);
	return (
		<div className="PrivateCompanyStatistics container">
			{/* // TODO Language */}
			<div className="d-flex border border-dark w-100">
				<PrivateCompanyPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>Statistics</h1>
					</div>
				</header>
				<div>
					<Card className="my-5">
						<Card.Header>Average delivery time:</Card.Header>
						<Card.Body>
							<Card.Text>{`${averageDeliveryTime}`}</Card.Text>
						</Card.Body>
					</Card>

					<Card className="my-5">
						<Card.Header>Average delivery path length</Card.Header>
						<Card.Body>
							<Form.Group>
								<Form.Control className="w-25" as="select" onChange={e => handleSelectChange(e)}>
									{options.map(({ value, label }) => (
										<option key={value} value={value}>
											{label}
										</option>
									))}
								</Form.Control>
							</Form.Group>
							<Card.Text></Card.Text>
							<Card.Text>{`${averageDeliveryPathLength}`}</Card.Text>
						</Card.Body>
					</Card>

					<Card className="my-5">
						<Card.Header>Number of delivered orders:</Card.Header>
						<Card.Body>
							<Card.Text>{`${numberOfDeliveredOrders}`}</Card.Text>
						</Card.Body>
					</Card>

					<Card className="my-5">
						<Card.Header>Number of not delivered orders:</Card.Header>
						<Card.Body>
							<Card.Text>{`${numberOfNotDeliveredOrders}`}</Card.Text>
						</Card.Body>
					</Card>

					<Card className="my-5">
						<Card.Header>Average order price:</Card.Header>
						<Card.Body>
							<Card.Text>{`${averageOrderPrice} $`}</Card.Text>
						</Card.Body>
					</Card>
				</div>
			</div>
		</div>
	);
};
