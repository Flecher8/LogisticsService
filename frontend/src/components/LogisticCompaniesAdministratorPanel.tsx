import { useState, useEffect, FC } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { ProfilePanel, ProfilePanelElement, ProfilePanelProps } from "./ProfilePanel";
import { useTranslationHelper } from "../helpers/translation/translationService";

export const LogisticCompaniesAdministratorPanel: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	// Panel elements
	const profilePanelElements: ProfilePanelElement[] = [
		{ name: t("Profile"), linkTo: "/LogisticCompaniesAdministratorProfile" },
		{ name: t("Drivers"), linkTo: "/LogisticCompanyAdministratorChangeDrivers" },
		{ name: t("Smart Devices"), linkTo: "/LogisticCompanyAdministratorSmartDevices" },
		{ name: t("Sensors"), linkTo: "/LogisticCompanyAdministratorSensors" },
		{ name: t("Active orders"), linkTo: "/LogisticCompanyAdministratorActiveOrders" },
		{ name: t("Delivered orders"), linkTo: "/LogisticCompanyAdministratorDeliveredOrders" },
		{ name: t("Cancelled orders"), linkTo: "/LogisticCompanyAdministratorCancelledOrders" }
	];

	// create a profile panel props object
	const profilePanelProps: ProfilePanelProps = {
		profilePanelElements
	};
	return (
		<div className="LogisticCompaniesAdministratorPanel">
			<ProfilePanel {...profilePanelProps} />
		</div>
	);
};
