import { useState, useEffect, FC } from "react";
import { Button, Card, Form } from "react-bootstrap";
import { LogisticCompanyPanel } from "../../components/LogisticCompanyPanel";
import { LogisticCompanyStatisticsService } from "../../api/services/LogisticCompanyStatisticsService";
import { useTranslationHelper } from "../../helpers/translation/translationService";

interface Option {
	value: string;
	label: string;
}

export const LogisticCompanyStatistics: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	const options: Option[] = [
		{ value: "km", label: t("km") },
		{ value: "mi", label: t("mi") }
	];

	const [averageDeliveryTime, setAverageDeliveryTime] = useState<string>("");
	const [averageDeliveryPathLength, setAverageDeliveryPathLength] = useState<number>(0);
	const [numberOfDeliveredOrders, setNumberOfDeliveredOrders] = useState<number>(0);
	const [numberOfNotDeliveredOrders, setNumberOfNotDeliveredOrders] = useState<number>(0);
	const [averageOrderPrice, setAverageOrderPrice] = useState<number>(0);

	const getAverageDeliveryTime = async (userId: number): Promise<void> => {
		try {
			const response: string = await LogisticCompanyStatisticsService.prototype.getAverageDeliveryTime(userId);
			setAverageDeliveryTime(response);
		} catch (err) {}
	};

	const getAverageDeliveryPathLength = async (userId: number, metric: string): Promise<void> => {
		try {
			const response: number = await LogisticCompanyStatisticsService.prototype.getAverageDeliveryPathLength(
				userId,
				metric
			);
			setAverageDeliveryPathLength(response);
		} catch (err) {}
	};

	const getNumberOfDeliveredOrders = async (userId: number): Promise<void> => {
		try {
			const response: number = await LogisticCompanyStatisticsService.prototype.getNumberOfDeliveredOrders(userId);
			setNumberOfDeliveredOrders(response);
		} catch (err) {}
	};

	const getNumberOfNotDeliveredOrders = async (userId: number): Promise<void> => {
		try {
			const response: number = await LogisticCompanyStatisticsService.prototype.getNumberOfNotDeliveredOrders(
				userId
			);
			setNumberOfNotDeliveredOrders(response);
		} catch (err) {}
	};

	const getAverageOrderPrice = async (userId: number): Promise<void> => {
		try {
			const response: number = await LogisticCompanyStatisticsService.prototype.getAverageOrderPrice(userId);
			setAverageOrderPrice(response);
		} catch (err) {}
	};

	const getStatistics = async (): Promise<void> => {
		const userId: number = parseInt(localStorage["userId"]);
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
		<div className="LogisticCompanyStatistics container">
			<div className="d-flex border border-dark w-100">
				<LogisticCompanyPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>{t("Statistics")}</h1>
					</div>
				</header>
				<div>
					<Card className="my-5">
						<Card.Header>{t("Average delivery time")}:</Card.Header>
						<Card.Body>
							<Card.Text>{`${averageDeliveryTime}`}</Card.Text>
						</Card.Body>
					</Card>

					<Card className="my-5">
						<Card.Header>{t("Average delivery path length")}</Card.Header>
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
						<Card.Header>{t("Number of delivered orders")}:</Card.Header>
						<Card.Body>
							<Card.Text>{`${numberOfDeliveredOrders}`}</Card.Text>
						</Card.Body>
					</Card>

					<Card className="my-5">
						<Card.Header>{"Number of not delivered orders"}:</Card.Header>
						<Card.Body>
							<Card.Text>{`${numberOfNotDeliveredOrders}`}</Card.Text>
						</Card.Body>
					</Card>

					<Card className="my-5">
						<Card.Header>{t("Average order price")}:</Card.Header>
						<Card.Body>
							<Card.Text>{`${averageOrderPrice} $`}</Card.Text>
						</Card.Body>
					</Card>
				</div>
			</div>
		</div>
	);
};
