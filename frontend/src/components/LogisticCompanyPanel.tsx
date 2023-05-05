import { useState, useEffect, FC } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { ProfilePanel, ProfilePanelElement, ProfilePanelProps } from "./ProfilePanel";

// Panel elements
const profilePanelElements: ProfilePanelElement[] = [
	{ name: "Profile", linkTo: "/LogisticCompanyPanel" }
	// { name: "Statistics", linkTo: "/PrivateCompanyStatistics" },
	// { name: "Active Orders", linkTo: "/PrivateCompanyActiveOrders" },
	// { name: "Delivered Orders", linkTo: "/PrivateCompanyDeliveredOrders" },
	// { name: "Cancelled Orders", linkTo: "/PrivateCompanyCancelledOrders" }
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
