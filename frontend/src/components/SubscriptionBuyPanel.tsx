import { useState, useEffect, FC } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { ProfilePanel, ProfilePanelElement, ProfilePanelProps } from "./ProfilePanel";
import { useTranslationHelper } from "../helpers/translation/translationService";

export const SubscriptionBuyPanel: FC = () => {
	const { t, changeLanguage } = useTranslationHelper();

	// Panel elements
	const profilePanelElements: ProfilePanelElement[] = [
		{ name: t("Profile"), linkTo: "/LogisticCompanyProfile" }
		// { name: t(""), linkTo: "/" },
	];

	// create a profile panel props object
	const profilePanelProps: ProfilePanelProps = {
		profilePanelElements
	};
	return (
		<div className="SubscriptionBuyPanel">
			<ProfilePanel {...profilePanelProps} />
		</div>
	);
};
