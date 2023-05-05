import { useState, useEffect, FC } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { ProfilePanel, ProfilePanelElement, ProfilePanelProps } from "./ProfilePanel";

// Panel elements
const profilePanelElements: ProfilePanelElement[] = [
	{ name: "Profile", linkTo: "/LogisticCompanyProfile" },
	{ name: "Statistics", linkTo: "/LogisticCompanyStatistics" },
	{ name: "Smart Devices", linkTo: "/LogisticCompanySmartDevices" },
	{ name: "Administrators", linkTo: "/LogisticCompanyChangeAdministrators" }
];

// create a profile panel props object
const profilePanelProps: ProfilePanelProps = {
	profilePanelElements
};

export const LogisticCompanyPanel: FC = () => {
	return (
		<div className="LogisticCompanyPanel">
			<ProfilePanel {...profilePanelProps} />
		</div>
	);
};
