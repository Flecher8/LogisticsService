import { useState, useEffect, FC } from "react";
import { Button, Card } from "react-bootstrap";
import { Link } from "react-router-dom";
import { AdminPanel } from "../../components/AdminPanel";
import { SystemAdmin, SystemAdminService } from "../../api/services/SystemAdminService";

export const SystemAdminProfile: FC = () => {
	const [profile, setProfile] = useState<SystemAdmin | null>(null);

	const getProfileData = async (): Promise<void> => {
		try {
			const userId: number = parseInt(localStorage["userId"]);
			const response: SystemAdmin | null = await SystemAdminService.prototype.getItem(userId);
			setProfile(response);
		} catch (err) {
			alert(err);
		}
	};

	useEffect(() => {
		getProfileData();
	}, []);
	return (
		<div className="SystemAdminProfile container">
			{/* // TODO Language */}
			<div className="d-flex border border-dark w-100">
				<AdminPanel />
			</div>
			<div>
				<header>
					<div className="text-center mt-5">
						<h1>Profile</h1>
					</div>
				</header>
				{profile && (
					<Card className="my-5">
						<Card.Header>Profile Information</Card.Header>
						<Card.Body>
							<Card.Text>{`Name: ${profile.firstName} ${profile.lastName}`}</Card.Text>
							<Card.Text>{`Email: ${profile.email}`}</Card.Text>
						</Card.Body>
					</Card>
				)}
			</div>
		</div>
	);
};
