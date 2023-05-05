import { useState, useEffect, FC } from "react";
import { Card } from "react-bootstrap";
import { LogisticCompanyRates } from "../api/services/RatesService";

interface RateCardProps {
	logisticCompanyRate: LogisticCompanyRates;
	selectedItemId: number;
	onSelectItem: (itemId: number) => void;
}

export const RateCard: FC<RateCardProps> = ({ logisticCompanyRate, selectedItemId, onSelectItem }) => {
	const isSelected = logisticCompanyRate.logisticCompanyId === selectedItemId;
	const cardClassName = `my-5 ${isSelected ? "border border-primary" : ""}`;
	const handleClick = () => {
		onSelectItem(logisticCompanyRate.logisticCompanyId);
	};
	return (
		<div className="RateCard container">
			<Card
				className={cardClassName}
				style={{ width: "18rem", boxShadow: "0px 2px 4px rgba(0, 0, 0, 0.25)", cursor: "pointer" }}
				onClick={handleClick}>
				<Card.Header>Logistic Company: {`${logisticCompanyRate.logisticCompanyName}`}</Card.Header>
				<Card.Body>
					<Card.Text>Price per km: {`${logisticCompanyRate.rate}`} $</Card.Text>
				</Card.Body>
			</Card>
		</div>
	);
};
