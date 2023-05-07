import { useState, useEffect, FC } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { ProfilePanel, ProfilePanelElement, ProfilePanelProps } from "./ProfilePanel";

// Panel elements
const profilePanelElements: ProfilePanelElement[] = [
	{ name: "Profile", linkTo: "/LogisticCompaniesAdministratorProfile" },
	{ name: "Drivers", linkTo: "/LogisticCompanyAdministratorChangeDrivers" },
	{ name: "Smart Devices", linkTo: "/LogisticCompanyAdministratorSmartDevices" },
	{ name: "Sensors", linkTo: "/LogisticCompanyAdministratorSensors" },
	{ name: "Active orders", linkTo: "/LogisticCompanyAdministratorActiveOrders" },
	{ name: "Delivered orders", linkTo: "/LogisticCompanyAdministratorDeliveredOrders" },
	{ name: "Cancelled orders", linkTo: "/LogisticCompanyAdministratorCancelledOrders" }
];

// create a profile panel props object
const profilePanelProps: ProfilePanelProps = {
	profilePanelElements
};

export const LogisticCompaniesAdministratorPanel: FC = () => {
	return (
		<div className="LogisticCompaniesAdministratorPanel">
			<ProfilePanel {...profilePanelProps} />
		</div>
	);
};
