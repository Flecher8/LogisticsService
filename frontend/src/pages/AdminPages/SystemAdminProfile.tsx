import { useState, useEffect, FC } from "react";
import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { AdminPanel } from "../../components/AdminPanel";

export const SystemAdminProfile: FC = () => {
	return (
		<div className="SystemAdminProfile container">
			{/* // TODO Language */}
			<div className="d-flex border border-dark w-100">
				<AdminPanel />
			</div>
			<div>
				<h1>Profile</h1>
			</div>
		</div>
	);
};
