import { useState, useEffect, FC } from "react";
import { Card } from "react-bootstrap";
import { useTranslationHelper } from "../helpers/translation/translationService";
import { SubscriptionType } from "../api/services/SubscriptionTypesService";
import { SubscriptionTypeCard } from "./SubscriptionTypeCard";

interface SubscriptionTypesListProps {
	subscriptionTypes: SubscriptionType[];
	selectedItemId: number;
	onSelectItem: (itemId: number) => void;
}

export const SubscriptionTypesList: FC<SubscriptionTypesListProps> = ({
	subscriptionTypes,
	selectedItemId,
	onSelectItem
}) => {
	const { t, changeLanguage } = useTranslationHelper();

	const handleSelectItem = (itemId: number) => {
		onSelectItem(itemId);
	};

	useEffect(() => {
		if (subscriptionTypes.length > 0 && subscriptionTypes[0].subscriptionTypeId !== undefined) {
			onSelectItem(subscriptionTypes[0].subscriptionTypeId);
		}
	}, []);
	return (
		<div className="SubscriptionTypesList container">
			<div className="d-flex flex-row align-items-start flex-fill flex-wrap">
				{subscriptionTypes ? (
					subscriptionTypes.map(subscriptionType => (
						<div key={subscriptionType.subscriptionTypeId} className="d-flex flex-fill flex-wrap">
							<SubscriptionTypeCard
								subscriptionType={subscriptionType}
								selectedItemId={selectedItemId}
								onSelectItem={handleSelectItem}
							/>
						</div>
					))
				) : (
					<p>{t("No data")}</p>
				)}
			</div>
		</div>
	);
};
