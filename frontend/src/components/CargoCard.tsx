import { useState, FC } from "react";
import { Button, Card } from "react-bootstrap";
import { Cargo } from "../api/services/OrdersService";

interface CargoProps {
	cargo: Cargo;
}

export const CargoCard: FC<CargoProps> = ({ cargo }: CargoProps) => {
	const [isMetric, setIsMetric] = useState(true);

	const formatDimensions = (value: number, isMetric: boolean): string => {
		const unit = isMetric ? "cm" : "inch";
		const formattedValue = isMetric ? value.toFixed(2) : (value / 2.54).toFixed(2);
		return `${formattedValue} ${unit}`;
	};

	const kgTolb: number = 2.20462;

	return (
		<Card>
			<Card.Header>Cargo</Card.Header>
			<Card.Body>
				<Card.Title>Name: {cargo.name}</Card.Title>
				<Card.Subtitle className="mb-2 text-muted">Description: {cargo.description}</Card.Subtitle>
				<Card.Text>
					Weight: {isMetric ? `${cargo.weight.toFixed(2)} kg` : `${(cargo.weight * kgTolb).toFixed(2)} lb`}
					<br />
					Dimensions:{" "}
					{`${formatDimensions(cargo.length, isMetric)} x ${formatDimensions(
						cargo.width,
						isMetric
					)} x ${formatDimensions(cargo.height, isMetric)}`}
				</Card.Text>
				<select value={isMetric ? "metric" : "imperial"} onChange={e => setIsMetric(e.target.value === "metric")}>
					<option value="metric">Metric</option>
					<option value="imperial">Imperial</option>
				</select>
			</Card.Body>
		</Card>
	);
};
