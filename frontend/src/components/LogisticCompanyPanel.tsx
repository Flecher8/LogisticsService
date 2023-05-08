import { useState, useEffect, FC } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { ProfilePanel, ProfilePanelElement, ProfilePanelProps } from "./ProfilePanel";
import { useTranslationHelper } from "../helpers/translation/translationService";

export const LogisticCompanyPanel: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	// Panel elements
	const profilePanelElements: ProfilePanelElement[] = [
		{ name: t("Profile"), linkTo: "/LogisticCompanyProfile" },
		{ name: t("Statistics"), linkTo: "/LogisticCompanyStatistics" },
		{ name: t("Smart Devices"), linkTo: "/LogisticCompanySmartDevices" },
		{ name: t("Administrators"), linkTo: "/LogisticCompanyChangeAdministrators" }
	];

	// create a profile panel props object
	const profilePanelProps: ProfilePanelProps = {
		profilePanelElements
	};
	return (
		<div className="LogisticCompanyPanel">
			<ProfilePanel {...profilePanelProps} />
		</div>
	);
};
