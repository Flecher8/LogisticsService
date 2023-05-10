import { useState, useEffect, FC } from "react";
import { Card } from "react-bootstrap";
import { useTranslationHelper } from "../helpers/translation/translationService";
import { SubscriptionType } from "../api/services/SubscriptionTypesService";

interface SubscriptionTypeCardProps {
	subscriptionType: SubscriptionType;
	selectedItemId: number;
	onSelectItem: (itemId: number) => void;
}

export const SubscriptionTypeCard: FC<SubscriptionTypeCardProps> = ({
	subscriptionType,
	selectedItemId,
	onSelectItem
}) => {
	const { t, changeLanguage } = useTranslationHelper();

	const isSelected = subscriptionType.subscriptionTypeId === selectedItemId;
	const cardClassName = `my-5 ${isSelected ? "border border-primary" : ""}`;
	const handleClick = () => {
		onSelectItem(subscriptionType.subscriptionTypeId ?? 0);
	};
	return (
		<div className="SubscriptionTypeCard container">
			<Card
				className={cardClassName}
				style={{ width: "18rem", boxShadow: "0px 2px 4px rgba(0, 0, 0, 0.25)", cursor: "pointer" }}
				onClick={handleClick}>
				<Card.Header>
					{t("Subscription type")}: {`${subscriptionType.subscriptionTypeName}`}
				</Card.Header>
				<Card.Body>
					<Card.Text>
						{t("Duration in days")}: {`${subscriptionType.durationInDays}`}{" "}
					</Card.Text>
					<Card.Text>
						{t("Price")}: {`${subscriptionType.price}`} $
					</Card.Text>
				</Card.Body>
			</Card>
		</div>
	);
};
