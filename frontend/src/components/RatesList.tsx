import { useState, useEffect, FC } from "react";
import { Card } from "react-bootstrap";
import { LogisticCompanyRates } from "../api/services/RatesService";
import { RateCard } from "./RateCard";
import { useTranslationHelper } from "../helpers/translation/translationService";

interface RatesListProps {
	logisticCompanyRates: LogisticCompanyRates[];
	selectedItemId: number;
	onSelectItem: (itemId: number) => void;
}

export const RatesList: FC<RatesListProps> = ({ logisticCompanyRates, selectedItemId, onSelectItem }) => {
	const { t, changeLanguage } = useTranslationHelper();

	const handleSelectItem = (itemId: number) => {
		onSelectItem(itemId);
	};

	useEffect(() => {
		if (logisticCompanyRates.length > 0) {
			onSelectItem(logisticCompanyRates[0].logisticCompanyId);
		}
	}, []);
	return (
		<div className="RateCard container">
			<div className="d-flex flex-row align-items-start flex-fill flex-wrap">
				{logisticCompanyRates ? (
					logisticCompanyRates.map(logisticCompanyRate => (
						<div key={logisticCompanyRate.logisticCompanyId} className="d-flex flex-fill flex-wrap">
							<RateCard
								logisticCompanyRate={logisticCompanyRate}
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
