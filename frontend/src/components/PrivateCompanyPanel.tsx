import { useState, useEffect, FC } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { ProfilePanel, ProfilePanelElement, ProfilePanelProps } from "./ProfilePanel";

// Panel elements
const profilePanelElements: ProfilePanelElement[] = [
	{ name: "Profile", linkTo: "/SystemAdminProfile" }
	// { name: "Profile", linkTo: "/SystemAdminProfile" },
];

// create a profile panel props object
const profilePanelProps: ProfilePanelProps = {
	profilePanelElements
};

export const PrivateCompanyPanel: FC = () => {
	return (
		<div className="PrivateCompanyPanel">
			<ProfilePanel {...profilePanelProps} />
		</div>
	);
};
