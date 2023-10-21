import { useState, useEffect, FC } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { ProfilePanel, ProfilePanelElement, ProfilePanelProps } from "./ProfilePanel";
import { useTranslationHelper } from "../helpers/translation/translationService";

export const PrivateCompanyPanel: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	// Panel elements
	const profilePanelElements: ProfilePanelElement[] = [
		{ name: t("Profile"), linkTo: "/PrivateCompanyProfile" },
		{ name: t("Statistics"), linkTo: "/PrivateCompanyStatistics" },
		{ name: t("Active Orders"), linkTo: "/PrivateCompanyActiveOrders" },
		{ name: t("Delivered Orders"), linkTo: "/PrivateCompanyDeliveredOrders" },
		{ name: t("Cancelled Orders"), linkTo: "/PrivateCompanyCancelledOrders" }
	];

	// create a profile panel props object
	const profilePanelProps: ProfilePanelProps = {
		profilePanelElements
	};
	return (
		<div className="PrivateCompanyPanel">
			<ProfilePanel {...profilePanelProps} />
		</div>
	);
};
