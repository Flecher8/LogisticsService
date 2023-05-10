import { useState, useEffect, FC } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { ProfilePanel, ProfilePanelElement, ProfilePanelProps } from "./ProfilePanel";
import { useTranslationHelper } from "../helpers/translation/translationService";

export const AdminPanel: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	// Panel elements
	const profilePanelElements: ProfilePanelElement[] = [
		{ name: t("Profile"), linkTo: "/SystemAdminProfile" },
		{ name: t("Subscription Types"), linkTo: "/AdminSubscriptionTypes" },
		{ name: t("Sensors"), linkTo: "/AdminSensors" },
		{ name: t("Smart Devices"), linkTo: "/AdminSmartDevices" },
		{ name: t("Logistic Companies"), linkTo: "/AdminLogisticCompanies" }
	];

	// create a profile panel props object
	const profilePanelProps: ProfilePanelProps = {
		profilePanelElements
	};

	return (
		<div className="AdminPanel">
			<ProfilePanel {...profilePanelProps} />
		</div>
	);
};
