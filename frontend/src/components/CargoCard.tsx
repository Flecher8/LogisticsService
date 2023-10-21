import { useState, FC } from "react";
import { Button, Card } from "react-bootstrap";
import { Cargo } from "../api/services/CargosService";
import { useTranslationHelper } from "../helpers/translation/translationService";

interface CargoProps {
	cargo: Cargo;
}

export const CargoCard: FC<CargoProps> = ({ cargo }: CargoProps) => {
	const { t, changeLanguage } = useTranslationHelper();

	const [isMetric, setIsMetric] = useState(true);

	const formatDimensions = (value: number, isMetric: boolean): string => {
		const unit = isMetric ? t("cm") : t("inch");
		const formattedValue = isMetric ? value.toFixed(2) : (value / 2.54).toFixed(2);
		return `${formattedValue} ${unit}`;
	};

	const kgTolb: number = 2.20462;

	return (
		<Card>
			<Card.Header>{t("Cargo")}</Card.Header>
			<Card.Body>
				<Card.Title>
					{t("Name")}: {cargo.name}
				</Card.Title>
				<Card.Subtitle className="mb-2 text-muted">
					{t("Description")}: {cargo.description}
				</Card.Subtitle>
				<Card.Text>
					{t("Weight")}:{" "}
					{isMetric ? `${cargo.weight.toFixed(2)} ${t("kg")}` : `${(cargo.weight * kgTolb).toFixed(2)} ${t("lb")}`}
					<br />
					{t("Dimensions")}:{" "}
					{`${formatDimensions(cargo.length, isMetric)} x ${formatDimensions(
						cargo.width,
						isMetric
					)} x ${formatDimensions(cargo.height, isMetric)}`}
				</Card.Text>
				<select value={isMetric ? "metric" : "imperial"} onChange={e => setIsMetric(e.target.value === "metric")}>
					<option value="metric">{t("Metric")}</option>
					<option value="imperial">{t("Imperial")}</option>
				</select>
			</Card.Body>
		</Card>
	);
};
