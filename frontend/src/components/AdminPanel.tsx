import { useState, useEffect, FC } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { ProfilePanel, ProfilePanelElement, ProfilePanelProps } from "./ProfilePanel";

// Panel elements
const profilePanelElements: ProfilePanelElement[] = [
	{ name: "Profile", linkTo: "/SystemAdminProfile" },
	{ name: "SubscriptionTypes", linkTo: "/AdminSubscriptionTypes" }
	// { name: "Logout", linkTo: "/logout" }
];

// create a profile panel props object
const profilePanelProps: ProfilePanelProps = {
	profilePanelElements
};

export const AdminPanel: FC = () => {
	return (
		<div className="AdminPanel">
			<ProfilePanel {...profilePanelProps} />
		</div>
	);
};
